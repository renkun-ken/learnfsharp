// Tuples
// http://msdn.microsoft.com/en-us/library/dd233200.aspx
// Tuple of two integers.
let a, b = 3, 4
let tuple1 = (1, 2)
// Triple of strings.
let tuple2 = ("one", "two", "three")
// Tuple of unknown types.
let tuple3 = (a, b)
// Tuple that has mixed types.
let tuple4 = ("one", 1, 2.0)
// Tuple of integer expressions.
let tuple5 = (a + 1, b + 1)
let (x, _) = (1, 2)
let c = fst(1, 2)
let d = snd(1, 2)
let third(_, _, c) = c

let distance point = 
    match point with
    | (x, y) -> sqrt(x ** 2.0 + y ** 2.0)
