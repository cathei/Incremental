# Incremental 📈
Incremental Deterministic Decimal Number type in C#

## Features
* 128-bit number type
  * 1-bit sign
  * 58-bit mantissa
  * 64-bit exponent
* Decimal representation (No binary rounding error)
* Deterministic math calculation
* Reasonable precision (17 decimal digits)
* HUGE representation range from 1E-(2^63) to 9.99E+(2^63-1)
  * Means you can have 9,223,372,036,854,775,807 zeros

## Comparison
| Category       | Incremental          | decimal   | BreakInfinity BigDouble |
|----------------|----------------------|-----------|------------------------|
| Size           | 16 bytes (long+long) | 16 bytes  | 16 bytes (double+long) |
| Representation | decimal              | decimal   | binary                 |
| Precision      | 17 digits            | 29 digits | 17 digits              |
| Deterministic? | Yes                  | Yes       | No                     |

