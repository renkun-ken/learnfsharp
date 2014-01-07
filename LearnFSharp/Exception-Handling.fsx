// Error handling and Debugging
// Asserting
let subtractUnsigned (x: uint32) (y: uint32) =
    assert (x > y)
    let z = x - y
    z

// This code does not generate an assertion failure.
let result1 = subtractUnsigned 2u 1u
// This code generates an assertion failure.
let result2 = subtractUnsigned 1u 2u

// try..with..
let divide2 x y =
    try
        Some(x / y)
    with :? System.DivideByZeroException as ex ->
        printfn "Exception! %s" (ex.Message)
        None

let divide3 x y report =
    try
        Some(x / y)
    with
    | ex when report ->
        printfn "Error: %s" (ex.ToString())
        None
    | ex when not report -> None

// Defining Exceptions
exception Error1 of string

exception Error2 of string * int

let function1 x y =
    try
        if x = y then raise(Error1("x"))
        else raise(Error2("x", 10))
    with
    | Error1(str) -> printfn "Error1 %s" str
    | Error2(str, i) -> printfn "Error2 %s %d" str i

function1 10 10
function1 9 2

// Raise Exceptions
let readData list =
    list |> List.iter(function
                | x when x >= 0 -> printfn "%d" x
                | x when x <= -3 -> failwith "Error"
                | _ -> printfn "Unusual value")

readData [ 1; 2; 3; 2; 1; 0; -1; -2; -3; -4 ]

let readData2 list =
    list |> List.iter(function
                | x when x >= 0 -> printfn "%d" x
                | x when x <= -3 -> raise(System.ArgumentException("It's wrong", "x"))
                | x -> printfn "Unusual value: %d" x)

readData2 [ 1; 2; 3; 2; 1; 0; -1; -2; -3; -4 ]

let readData3 list =
    list |> List.iter(function
                | x when x >= 0 -> printfn "%d" x
                | x when x <= -3 -> invalidArg "x" "Invalid argument"
                | x -> printfn "Unusual value: %d" x)

readData3 [ 1; 2; 3; 2; 1; 0; -1; -2; -3; -4 ]

// Catch Exceptions
let tryError1 =
    try
        readData3 [ 1; 2; 3; 2; 1; 0; -1; -2; -3; -4 ]
    with :? System.ArgumentException as ex -> printfn "Incorrect argument: %s" ex.ParamName

// try..finally
let divide x y =
    let stream: System.IO.FileStream = System.IO.File.Create(@"d:\data\test\test.txt")
    let writer: System.IO.StreamWriter = new System.IO.StreamWriter(stream)
    try
        writer.WriteLine("test1")
        Some(x / y)
    finally
        writer.Flush()
        printfn "Closing stream"
        stream.Close()

let result =
    try
        divide 100 0
    with :? System.DivideByZeroException ->
        printfn "Exception handled."
        None

exception InnerError of string

exception OuterError of string

let function2 x y =
    try
        try
            if x = y then raise(InnerError("inner"))
            else raise(OuterError("outer"))
        with InnerError(str) -> printfn "Error1 %s" str
    finally
        printfn "Always print this."

let function3 x y =
    try
        function2 x y
    with OuterError(str) -> printfn "Error2 %s" str

function3 100 100
function3 100 10