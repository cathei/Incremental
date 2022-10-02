# Incremental 📈
Incremental Deterministic Decimal Number type in C#

> **Warning**  
> The product is under development, not production ready yet.

## Features
* 128-bit number type
  * 1-bit sign
  * 57-bit mantissa
  * 64-bit exponent
* Decimal representation (No binary rounding error)
* Deterministic math calculation
* Reasonable precision (17 decimal digits)
* HUGE representation range from 1E-(2^63) to 9.99E+(2^63-1)
  * Means you can have 9,223,372,036,854,775,807 zeros

## Installation
Clone this repository and copy `Incremental.cs` and `Incremental.Internal.cs` to your project.

## Comparison
| Category         | Incremental          | C# decimal       | BreakInfinity BigDouble   |
|------------------|----------------------|---------------|---------------------------|
| Size             | 16 bytes (long+long) | 16 bytes      | 16 bytes (double+long)    |
| Representation   | decimal (base 10)             | decimal (base 10)       | binary (base 2)                   |
| Deterministic?   | Yes                  | Yes           | No                        |
| Precision        | 17 digits            | 28~29 digits     | 15~17 digits                 |
| Exponent (Up to) | 64 bits (E+2^63-1)   | 8 bits (E+28) | 64+11 bits (E+2^63-1+308) |

Since `Incremental` uses decimal representation, you are able to write exact number as `0.1`,
which is actually `0.1000000000000000055511151231257827021181583404541015625` when you write in `double` type.
Due to this behaviour, `Incremental` does not suffer from binary rounding errors.
It would be a better choice when you want to use same type for incremental currencies and regular ones.

`Incremental` also support deterministic calculation across platforms.
For example, you could use it for input-synced lockstep scenario with incremental stats.

Overall, you should consider using `Incremental` when you want very big number with decimal places and/or deterministic behaviour.

## Benchmarks
You can see the benchmark results in [Incremental.Benchmarks](Incremental.Benchmarks).

## Implementation Details
### Data
`Incremental` type has two `long` variable, `Mantissa` and `Exponent`.

`Mantissa` is normalized decimal representation of fixed point number
where `10,000,000,000,000,000` (or `Unit`) is `1` and `99,990,000,000,000,000` is `9.999`.
`Mantissa` is signed and can represent negative number.
Absolute value of `Mantissa` is always `Unit <= x < Unit * 10`, except when it is `0`.

`Exponent` is power of 10 value to multiply `Mantissa`. `Exponent` is signed and can represent smaller value than `1`.

### Multiplication
128-bit math is not native in 64-bit system, `Incremental` does partial multiplication.

For multiplication, the result mantissa would be `a * b / Unit`.
Since result of `a * b` will overflow in 64-bit size, `Incremental` has to calculate `a / Unit` first then multiply by `b`.

We can represent a fractal part of `1 / Unit` (`0x0.000...734ACA5F6226F0ADA6...`), then by shifting it left to remove leading zeros for precision.
Then we get magic number `0xE69594BEC44DE15B` which is `(1 / Unit) << 53 << 64`.

We can remove unused bit of `a`'s mantissa with shifting it's value left by 7 bits, then multiply with magic number.
The full result will be 128-bit, but we are only need to calculate upper 64-bit since the result is already shifted.
Taking upper half would be same as shift total result to right by 64, so the result is `(a / Unit) << 60`.

As same as earlier, we can shift `b` by 7 bits, multiply with the previous result then take the upper 64-bit.
The result is `(a / Unit * b) << 3`, so by shifting 3 bits to right we can get the final result.

The procedural is fast because only multiplication is involved. It is similar to what compiler does to optimize division.
Further reading: [Explanation on StackOverflow](https://stackoverflow.com/questions/28868367/getting-the-high-part-of-64-bit-integer-multiplication)

### Division
As same reason as multiplication, we have to do partial division.
However partial division is not simple as multiplication, we cannot simply choose to take upper bit and divide.

While other algorithm is required for full division of 128-bit dividend and 64-bit divisor,
Using some unused bits of `Incremental` data type, we can calculate the quotient with multiple 64-bit division.

By removing leading zero of dividend and trailing zeros of divisor, we can do partial division and get intermediate quotient.
Shift and add the quotient to result. If there is remainder, we can repeat the procedure until there is no remainder or bits are not significant anymore.

Finally, doing multiplication between binary quotient and `Unit`, we can get normalized mantissa of the quotient.
