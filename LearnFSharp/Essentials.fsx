// Essentials
// Defining values
// http://msdn.microsoft.com/en-us/library/dd233230.aspx
// Binding statements and Primitive types
let x = 1
let y = x + 1
let pi = 3.14
let xy = x * y
let pis = pi ** 3.0
let pxy = pi * float x * float y
let pid = 3.14
let isTrue = false
// All values are immutable unless mutable keyword is used
let mutable val1 = 10.0
val1 <- 20.0 // assign a value to a mutable

// Conversion
let double1 = double "123.23"
let float2 = float "123"
let int3 = int "123"
// Using built-in functions
let sin1 = sin pi
let exp1 = exp 2.0
let sin2 = sin(pi * 1.5)
let log1 = log 2.0
let log2 = log10 100.0
let sqrt1 = sqrt 4.0
let pown1 = pown 2 8
// Defining Functions
let diameter r = 2.0 * pi * r
let area r = pi * r ** 2.0

let fun1 x y z = 
    let p1 = x + y
    let p2 = y + z
    let p3 = x * y * z
    (p1 + p2) / p3

let inUnitCircle x y = 
    let distance = sqrt(x ** 2.0 + y ** 2.0)
    distance <= 1.0

let pfun1 = fun1 2 3
let d1 = diameter 2.0
let a1 = area 3.0
let add10(x: float) = x + 10.0
let add10Days(date: System.DateTime) = date.AddDays(10.0)
let addDays (date: System.DateTime) days = date.AddDays(days)
let addDays2(date: System.DateTime, days) = date.AddDays(days)
// Strings
let str1 = "Hello"
let char1 = 'P'
let str2 = "Hello\nThis is a new line."
let str2char = str2.[0]
let str2substr = str2.[0..5]
let str3 = """
    Hello,
    this is a string.
    """

// Using primitive functions
// http://fsharpforfunandprofit.com/posts/printf/
printfn "The answer to everything is %d" 42
printfn "%s is an %d-year-old %s of height %3.1f cm" "James" 18 "boy" 172.54
printfn "The count of %s is %d, which cost %.2f in total" "apples" 20 35.4

let str4 = sprintf "The number is %d" 100
let strlen4 = String.length str4
// Using .NET member functions
let str1len = str1.Length
// Tuples
let p1 = ("A", 23)
let p2 = ("B", 24)
let rec1 = ("A1", 50, System.DateTime(2013, 10, 1))
let name1, num1 = p1
// Array
let numarray = [| 1; 2; 3 |]

let numarray1 = 
    [| for i in 1..10 -> i * i |]

let numarray2 = 
    [| for i in 1.0..0.5..5.0 -> i ** 2.0 |]

numarray.[0] <- 2

// List
let numlist = [ 1; 2; 3 ]

let numlist1 = 
    [ for i in 1..10 -> i * i ]

let numlist2 = 
    [ for i in 1.0..0.5..5.0 -> i ** 2.0 ]

let numlist3 = 0 :: numlist
let numlist4 = numlist @ numlist1

let numlist5 = 
    [ for i in 1..10 do
          let x = i + 1
          let y = float i ** 2.0
          yield x, y ]

// Dictionary
let dict1 = 
    dict [ ("A1", 100)
           ("A2", 300)
           ("A5", 150) ]

let entry1 = dict1.["A1"]
// Options
let op1 = Some 2.0
let op2 = None

// Sequences
let seq1 = 
    seq { 
        for i in 1..100 -> i, 2 * i + 1, (float i) ** 2.0 / 3.0
    }

// Recursion
let rec funr1 x = 
    if x <= 10 then x + funr1 1
    else 1

// Pattern Matching
let function1 x = 
    match x with
    | a, b -> a + b

// Discriminate Unions
type Gender = 
    | Male
    | Female

// Records
type Person = 
    { Name: string
      Gender: Gender
      Age: int }

let person1 = 
    { Name = "Ken"
      Gender = Male
      Age = 23 }

let person2 = 
    { Name = "James"
      Gender = Male
      Age = 24 }

let person3 = 
    { Name = "Penny"
      Gender = Female
      Age = 22 }

let persons = [ person1; person2; person3 ]

for person in persons do
    printfn "%A" person
