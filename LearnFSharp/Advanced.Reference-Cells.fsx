// Reference Cells
// Reference cells are storage locations that enable you to create mutable values with reference semantics.
// http://msdn.microsoft.com/en-us/library/dd233186.aspx
// Declare a reference.
let refVar = ref 6

// Change the value referred to by the reference.
refVar := 50
// Dereference by using the ! operator.
printfn "%d" !refVar

let xRef: int ref = ref 10

printfn "%d" (xRef.Value)
printfn "%d" (xRef.contents)
xRef.Value <- 11
printfn "%d" (xRef.Value)
xRef.contents <- 12
printfn "%d" (xRef.contents)

type Incrementor(delta) =
    member this.Increment(i: int byref) = i <- i + delta

let incrementor = new Incrementor(1)
let mutable myDelta1 = 10

incrementor.Increment(ref myDelta1)
// Prints 10:
printfn "%d" myDelta1

let mutable myDelta2 = 10

incrementor.Increment(&myDelta2)
// Prints 11:
printfn "%d" myDelta2

let refInt = ref 10

incrementor.Increment(refInt)
// Prints 11:
printfn "%d" !refInt

// Print all the lines read in from the console.
let PrintLines1() =
    let mutable finished = false
    while not finished do
        match System.Console.ReadLine() with
        | null -> finished <- true
        | s -> printfn "line is: %s" s

// Attempt to wrap the printing loop into a
// sequence expression to delay the computation.
let PrintLines2() =
    seq {
        let mutable finished = false
        // Compiler error:
        while not finished do
            match System.Console.ReadLine() with
            | null -> finished <- true
            | s -> yield s
    }

// You must use a reference cell instead.
let PrintLines3() =
    seq {
        let finished = ref false
        while not !finished do
            match System.Console.ReadLine() with
            | null -> finished := true
            | s -> yield s
    }