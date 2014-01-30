// Enumerations
// Enumerations, also known as enums, , are integral types where labels are assigned to a subset of the values.
// http://msdn.microsoft.com/en-us/library/dd233216.aspx
// Declaration of an enumeration.
type Color = 
    | Red = 0
    | Green = 1
    | Blue = 2

// Use of an enumeration.
let col1 : Color = Color.Red
let n = int col1
let col2 = enum<Color> (3)
