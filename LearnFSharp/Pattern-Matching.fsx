// Match Expressions
// http://msdn.microsoft.com/en-us/library/dd233242.aspx
let list1 = [ 1; 5; 100; 450; 788 ]

let rec printList list = 
    match list with
    | head :: tail -> 
        printf "%d " head
        printList tail
    | [] -> printfn ""

let filter123 x = 
    match x with
    | 1 | 2 | 3 -> printfn "Found 1, 2, or 3!"
    | a -> printfn "%d" a

let filter10 x = 
    match x with
    | a when a <= 10 -> printfn "Found %d no greater than 10" a
    | a -> printfn "Found %d" a

let filter123f = 
    function 
    | 1 | 2 | 3 -> printfn "Found 1, 2, or 3!"
    | a -> printfn "%d" a

let filter10f = 
    function 
    | a when a <= 10 -> printfn "Found %d no greater than 10" a
    | a -> printfn "Found %d" a

let rangeTest testValue mid size = 
    match testValue with
    | var1 when var1 >= mid - size / 2 && var1 <= mid + size / 2 -> "In"
    | _ -> "Out"

rangeTest 10 20 5
rangeTest 10 20 10
rangeTest 10 20 40

let detectValue point target = 
    match point with
    | (a, b) when a = target && b = target -> printfn "Both values match target %d." target
    | (a, b) when a = target -> printfn "First value matched target in (%d, %d)" target b
    | (a, b) when b = target -> printfn "Second value matched target in (%d, %d)" a target
    | _ -> printfn "Neither value matches target."

detectValue (0, 0) 0
detectValue (1, 0) 0
detectValue (0, 10) 0
detectValue (10, 15) 0

module Circle = 
    type Position = 
        | In
        | On
        | Out
    
    let isInCircle point circleCenter circleRadius = 
        match point, circleCenter with
        | (x, y), (a, b) when (x - a) ** 2.0 + (y - b) * 2.0 < circleRadius ** 2.0 -> In
        | (x, y), (a, b) when (x - a) ** 2.0 + (y - b) * 2.0 = circleRadius ** 2.0 -> On
        | _ -> Out

// Pattern matching
// Patterns are rules for transforming input data
// http://msdn.microsoft.com/en-us/library/dd547125.aspx
// Constant Patterns
[<Literal>]
let Three = 3

let filter123a x = 
    match x with
    // The following line contains literal patterns combined with an OR pattern.
    | 1 | 2 | Three -> printfn "Found 1, 2, or 3!"
    // The following line contains a variable pattern.
    | var1 -> printfn "%d" var1

for x in 1..10 do
    filter123a x

type Color = 
    | Red = 0
    | Green = 1
    | Blue = 2

let printColorName (color : Color) = 
    match color with
    | Color.Red -> printfn "Red"
    | Color.Green -> printfn "Green"
    | Color.Blue -> printfn "Blue"
    | _ -> ()

printColorName Color.Red
printColorName Color.Green
printColorName Color.Blue

// Identifier Patterns
let printOption (data : int option) = 
    match data with
    | Some var1 -> printfn "%d" var1
    | None -> ()

type PersonName = 
    | FirstOnly of string
    | LastOnly of string
    | FirstLast of string * string

let constructQuery personName = 
    match personName with
    | FirstOnly(firstName) -> printf "May I call you %s?" firstName
    | LastOnly(lastName) -> printf "Are you Mr. or Ms. %s?" lastName
    | FirstLast(firstName, lastName) -> printf "Are you %s %s?" firstName lastName

// Variable Patterns
let function1 x = 
    match x with
    | (var1, var2) when var1 > var2 -> printfn "%d is greater than %d" var1 var2
    | (var1, var2) when var1 < var2 -> printfn "%d is less than %d" var1 var2
    | (var1, var2) -> printfn "%d equals %d" var1 var2

function1 (1, 2)
function1 (2, 1)
function1 (0, 0)

// as Pattern
let (var1, var2) as tuple1 = (1, 2)

printfn "%d %d %A" var1 var2 tuple1

// OR Pattern
let detectZeroOR point = 
    match point with
    | (0, 0) | (0, _) | (_, 0) -> printfn "Zero found."
    | _ -> printfn "Both nonzero."

detectZeroOR (0, 0)
detectZeroOR (1, 0)
detectZeroOR (0, 10)
detectZeroOR (10, 15)

// AND Pattern
let detectZeroAND point = 
    match point with
    | (0, 0) -> printfn "Both values zero."
    | (var1, var2) & (0, _) -> printfn "First value is 0 in (%d, %d)" var1 var2
    | (var1, var2) & (_, 0) -> printfn "Second value is 0 in (%d, %d)" var1 var2
    | _ -> printfn "Both nonzero."

detectZeroAND (0, 0)
detectZeroAND (1, 0)
detectZeroAND (0, 10)
detectZeroAND (10, 15)

// Cons Pattern
let list2 = [ 1; 2; 3; 4 ]

// This example uses a cons pattern and a list pattern.
let rec printList2 l = 
    match l with
    | head :: tail -> 
        printf "%d " head
        printList tail
    | [] -> printfn ""

printList list2

// List Pattern
let listLength list = 
    match list with
    | [] -> 0
    | [ _ ] -> 1
    | [ _; _ ] -> 2
    | [ _; _; _ ] -> 3
    | _ -> List.length list

printfn "%d" (listLength [ 1 ])
printfn "%d" (listLength [ 1; 1 ])
printfn "%d" (listLength [ 1; 1; 1 ])
printfn "%d" (listLength [])

// Array Pattern
let vectorLength vec = 
    match vec with
    | [| var1 |] -> var1
    | [| var1; var2 |] -> sqrt (var1 * var1 + var2 * var2)
    | [| var1; var2; var3 |] -> sqrt (var1 * var1 + var2 * var2 + var3 * var3)
    | _ -> failwith "vectorLength called with an unsupported array size of %d." (vec.Length)

printfn "%f" (vectorLength [| 1. |])
printfn "%f" (vectorLength [| 1.; 1. |])
printfn "%f" (vectorLength [| 1.; 1.; 1. |])
printfn "%f" (vectorLength [||])

// Parenthesized Pattern
let countValues list value = 
    let rec checkList list acc = 
        match list with
        | (elem1 & head) :: tail when elem1 = value -> checkList tail (acc + 1)
        | head :: tail -> checkList tail acc
        | [] -> acc
    checkList list 0

let result = 
    countValues [ for x in -10..10 -> x * x - 4 ] 0

printfn "%d" result

// Tuple Pattern
let detectZeroTuple point = 
    match point with
    | (0, 0) -> printfn "Both values zero."
    | (0, var2) -> printfn "First value is 0 in (0, %d)" var2
    | (var1, 0) -> printfn "Second value is 0 in (%d, 0)" var1
    | _ -> printfn "Both nonzero."

detectZeroTuple (0, 0)
detectZeroTuple (1, 0)
detectZeroTuple (0, 10)
detectZeroTuple (10, 15)

// Record Pattern
type MyRecord = 
    { Name : string
      ID : int }

let IsMatchByName record1 (name : string) = 
    match record1 with
    | { MyRecord.Name = nameFound; MyRecord.ID = _ } when nameFound = name -> true
    | _ -> false

let recordX = 
    { Name = "Parker"
      ID = 10 }

let isMatched1 = IsMatchByName recordX "Parker"
let isMatched2 = IsMatchByName recordX "Hartono"

module Stock = 
    open System
    open System.IO
    open System.Net
    
    type Tick = 
        { Date : System.DateTime
          Open : float
          High : float
          Low : float
          Close : float
          Volume : float
          AdjClose : float }
    
    let url = "http://ichart.finance.yahoo.com/table.csv?s="
    
    let download quote = 
        use client = new WebClient()
        client.DownloadString(url + quote)
    
    let load quote n = 
        let ticks = 
            let data = download quote
            let lines = data.Split([| '\n' |], StringSplitOptions.RemoveEmptyEntries)
            lines
            |> Seq.skip 1
            |> Seq.truncate n
            |> Seq.map (fun line -> 
                   match line.Split(',') with
                   | [| date; ``open``; high; low; close; volume; adjClose |] -> 
                       { Date = DateTime.Parse(date)
                         Open = float ``open``
                         High = float high
                         Low = float low
                         Close = float close
                         Volume = float volume
                         AdjClose = float adjClose }
                   | _ -> raise (System.InvalidOperationException()))
            |> Seq.toArray
        ticks
    
    let goog = load "GOOG" 300
    
    let highGrowth = 
        goog
        |> Array.filter (function 
               | { Close = c } when c > 120.00 -> true
               | _ -> false)
        |> Array.map (fun tick -> (tick.Date, tick.Close))

// Patterns that have type annotations
let detect1 x = 
    match x with
    | 1 -> printfn "Found a 1!"
    | (var1 : int) -> printfn "%d" var1

detect1 0
detect1 1

// Type Test Pattern
let checkObject (o : obj) = 
    match o with
    | :? string as str -> printfn "String \"%s\"" str
    | :? int as num -> printfn "Integer %d" num
    | :? float as num -> printfn "Float number %f" num
    | x -> printfn "Object %A" x

// Null Pattern
let ReadFromFile(reader : System.IO.StreamReader) = 
    match reader.ReadLine() with
    | null -> 
        printfn "\n"
        false
    | line -> 
        printfn "%s" line
        true

let fs = System.IO.File.Open(@"d:\data\book\sh000300.csv", System.IO.FileMode.Open)
let sr = new System.IO.StreamReader(fs)

while ReadFromFile(sr) = true do
    ()
sr.Close()

// Active Patterns
// http://msdn.microsoft.com/en-us/library/dd233248.aspx
let (|Even|Odd|) input = 
    if input % 2 = 0 then Even
    else Odd

let TestNumber input = 
    match input with
    | Even -> printfn "%d is even" input
    | Odd -> printfn "%d is odd" input

let isEven = 
    function 
    | Even -> true
    | Odd -> false

open System.Drawing

let (|RGB|) (col : System.Drawing.Color) = (col.R, col.G, col.B)
let (|HSB|) (col : System.Drawing.Color) = (col.GetHue(), col.GetSaturation(), col.GetBrightness())

let printRGB (col : System.Drawing.Color) = 
    match col with
    | RGB(r, g, b) -> printfn " Red: %d Green: %d Blue: %d" r g b

let printHSB (col : System.Drawing.Color) = 
    match col with
    | HSB(h, s, b) -> printfn " Hue: %f Saturation: %f Brightness: %f" h s b

let printAll col colorString = 
    printfn "%s" colorString
    printRGB col
    printHSB col

printAll Color.Red "Red"
printAll Color.Black "Black"
printAll Color.White "White"
printAll Color.Gray "Gray"
printAll Color.BlanchedAlmond "BlanchedAlmond"

// Partial Active Patterns
let (|Integer|_|) (str : string) = 
    let mutable intvalue = 0
    if System.Int32.TryParse(str, &intvalue) then Some(intvalue)
    else None

let (|Float|_|) (str : string) = 
    let mutable floatvalue = 0.0
    if System.Double.TryParse(str, &floatvalue) then Some(floatvalue)
    else None

let parseNumeric str = 
    match str with
    | Integer i -> printfn "%d : Integer" i
    | Float f -> printfn "%f : Floating point" f
    | _ -> printfn "%s : Not matched." str

parseNumeric "1.1"
parseNumeric "0"
parseNumeric "0.0"
parseNumeric "10"
parseNumeric "Something else"

// Parametrized Active Patterns
let (|TestNumber|_|) benchmark num = 
    if benchmark > 0 then 
        if num > benchmark then Some("Big")
        elif num < benchmark then Some("Small")
        else None
    else None

let testNumber n = 
    match n with
    | TestNumber 5 x -> printfn "High standard, %s number" x
    | TestNumber 1 x -> printfn "Low standard, %s number" x
    | _ -> printfn "Invalid standard"

open System.Text.RegularExpressions

// ParseRegex parses a regular expression and returns a list of the strings that match each group in
// the regular expression.
// List.tail is called to eliminate the first element in the list, which is the full matched expression,
// since only the matches for each group are wanted.
let (|ParseRegex|_|) regex str = 
    let m = Regex(regex).Match(str)
    if m.Success then 
        Some(List.tail [ for x in m.Groups -> x.Value ])
    else None

// Three different date formats are demonstrated here. The first matches two-
// digit dates and the second matches full dates. This code assumes that if a two-digit
// date is provided, it is an abbreviation, not a year in the first century.
let parseDate str = 
    match str with
    | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{1,2})$" [ Integer m; Integer d; Integer y ] -> 
        new System.DateTime(y + 2000, m, d)
    | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{3,4})" [ Integer m; Integer d; Integer y ] -> new System.DateTime(y, m, d)
    | ParseRegex "(\d{1,4})-(\d{1,2})-(\d{1,2})" [ Integer y; Integer m; Integer d ] -> new System.DateTime(y, m, d)
    | _ -> new System.DateTime()

let dt1 = parseDate "12/22/08"
let dt2 = parseDate "1/1/2009"
let dt3 = parseDate "2008-1-15"
let dt4 = parseDate "1995-12-28"

printfn "%s %s %s %s" (dt1.ToString()) (dt2.ToString()) (dt3.ToString()) (dt4.ToString())
