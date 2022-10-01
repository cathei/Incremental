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
| Category         | Incremental          | decimal       | BreakInfinity BigDouble   |
|------------------|----------------------|---------------|---------------------------|
| Size             | 16 bytes (long+long) | 16 bytes      | 16 bytes (double+long)    |
| Representation   | decimal              | decimal       | binary                    |
| Deterministic?   | Yes                  | Yes           | No                        |
| Precision        | 17 digits            | 29 digits     | 17 digits                 |
| Exponent (Up to) | 64 bits (E+2^63-1)   | 8 bits (E+28) | 64+11 bits (E+2^63-1+308) |

Since `Incremental` uses decimal representation, you are able to write exact number as `0.1`,
which is actually `0.1000000000000000055511151231257827021181583404541015625` when you write in `double` type.
Due to this behaviour, `Incremental` does not suffer from binary rounding errors.
It would be a better choice when you want to use same type for incremental currencies and regular ones.

`Incremental` also support deterministic calculation across platforms.
For example, you could use it for input-synced lockstep scenario with incremental stats.

Overall, you should consider using `Incremental` when you want very big number with decimal places and/or deterministic behaviour.

