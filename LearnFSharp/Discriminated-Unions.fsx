// Discriminated Unions
// Discriminated unions provide support for values that can be one of a number of named cases, possibly each with different values and types.
// http://msdn.microsoft.com/en-us/library/dd233226.aspx
// Option
// type Option<'a> =
// | Some of 'a
// | None
let opt1 = Some(10.0)
let opt2 = Some("string")
let opt3 = None

let printValue opt =
    match opt with
    | Some x -> printfn "%A" x
    | None -> printfn "No value."

let val1 = System.Int32.TryParse("120")

match val1 with
| true, value -> printfn "%d" value
| _ -> printfn "Not integer"

module Shape =
    type Shape =
        | Circle of float
        | Square of float
        | Rectangle of float * float
        | Triangle of float * float * float

    let shape1: Shape = Rectangle(10.0, 20.0)
    let pi = 3.141592654

    let area shape =
        match shape with
        | Circle radius -> pi * radius ** 2.0
        | Square s -> s * s
        | Rectangle(h, w) -> h * w
        | Triangle(a, b, c) -> // Heron's formula

            let s = (a + b + c) / 2.0
            sqrt(s * (s - a) * (s - b) * (s - c))

    let myShapes =
        [ Circle(2.0)
          Circle(3.0)
          Triangle(1.0, 1.5, 0.8)
          Rectangle(1.5, 2.5)
          Triangle(3.5, 3.6, 3.4) ]

    let myShapeAreas = myShapes |> List.map(fun shape -> (shape, area shape))

module Tree =
    type Tree =
        | Leaf of float
        | Node of float * Tree * Tree

    let rec sumTree tree =
        match tree with
        | Leaf x -> x
        | Node(value, left, right) -> value + sumTree(left) + sumTree(right)

    let myTree =
        Node
            (1.0, Node(2.0, Node(1.5, Leaf(0.5), Leaf(0.6)), Node(2.5, Leaf(0.8), Leaf(0.4))),
             Node(3.5, Leaf(0.5), Leaf(2.5)))
    let resultSumTree = sumTree myTree

module Expression =
    type Expression =
        | Number of int
        | Add of Expression * Expression
        | Multiply of Expression * Expression
        | Variable of string

    let rec Evaluate (env: Map<string, int>) exp =
        match exp with
        | Number n -> n
        | Add(x, y) -> Evaluate env x + Evaluate env y
        | Multiply(x, y) -> Evaluate env x * Evaluate env y
        | Variable id -> env.[id]

    let environment =
        Map.ofList [ "a", 1
                     "b", 2
                     "c", 3 ]

    // Create an expression tree that represents
    // the expression: a + 2 * b.
    let expressionTree1 = Add(Variable "a", Multiply(Number 2, Variable "b"))
    // Evaluate the expression a + 2 * b, given the
    // table of values for the variables.
    let result = Evaluate environment expressionTree1

module Bank1 =
    type Transaction =
        | Deposit
        | Withdrawal

    let transactionTypes = [ Deposit; Deposit; Withdrawal ]
    let transactionAmouts = [ 100.00; 200.00; 150.00 ]
    let initialBalance = 200.00

    let endingBalance =
        List.fold2 (fun acc ttype tamount ->
            match ttype with
            | Deposit -> acc + tamount
            | Withdrawal -> acc - tamount) initialBalance transactionTypes transactionAmouts

module Bank2 =
    type Transaction =
        | Deposit
        | Withdrawal

    let transactions =
        [ (Deposit, 100.00)
          (Deposit, 200.00)
          (Withdrawal, 150.00) ]

    let initialBalance = 200.00

    let endingBalance =
        List.fold (fun acc trans ->
            match trans with
            | Deposit, x -> acc + x
            | Withdrawal, x -> acc - x) initialBalance transactions

module Bank3 =
    type Transaction =
        | Deposit
        | Withdrawal
        | Interest

    let transactions =
        [ (Deposit, 100.00)
          (Deposit, 200.00)
          (Withdrawal, 150.00)
          (Interest, 0.05 / 12.0)
          (Withdrawal, 50.00) ]

    let initialBalance = 0.00

    let endingBalance =
        List.fold (fun acc trans ->
            match trans with
            | Deposit, x -> acc + x
            | Withdrawal, x -> acc - x
            | Interest, r -> acc * (1.0 + r)) initialBalance transactions

module Bank4 =
    type Transaction =
        | Deposit of float
        | Withdrawal of float
        | Interest of float
        | Fee of float

    let transactions =
        [ Deposit(100.00)
          Deposit(200.00)
          Withdrawal(150.00)
          Interest(0.042 / 12.0)
          Fee(0.01)
          Deposit(500.00)
          Interest(0.055 / 12.0)
          Fee(0.01)
          Withdrawal(250.00)
          Interest(0.045 / 12.0)
          Fee(0.01) ]

    let initialBalance = 0.00

    let endingBalance =
        List.fold (fun acc trans ->
            match trans with
            | Deposit(x) -> acc + x
            | Withdrawal(x) -> acc - x
            | Interest(r) -> acc * (1.0 + r)
            | Fee(r) -> acc * (1.0 - r)) initialBalance transactions