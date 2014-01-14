// Records
// http://msdn.microsoft.com/en-us/library/dd233184.aspx
type Point =
    { x: float
      y: float
      z: float }
    member a.length = a.x + a.y

type Customer =
    { First }

let myPoint =
    { x = 1.0
      y = 1.0
      z = -1.0 }

let myPoint1 =
    { Point.x = 1.0
      y = 1.0
      z = 0.0 }

let customer1 =
    { First = "John"
      Last = "Smith"
      SSN = 1234u
      AccountNumber = 165002u }

type Car =
    { Manufacturer: string
      Model: string
      mutable Miles: uint32 }

let myCar =
    { Manufacturer = "BMW"
      Model = "A8"
      Miles = 120u }

myCar.Miles <- myCar.Miles + 21u

let myCar1 = { myCar with Miles = 5u }

let evaluatePoint(point: Point) =
    match point with
    | { x = 0.0; y = 0.0; z = 0.0 } -> printfn "Point is at the origin."
    | { x = xVal; y = 0.0; z = 0.0 } -> printfn "Point is on the x-axis. Value is %f." xVal
    | { x = 0.0; y = yVal; z = 0.0 } -> printfn "Point is on the y-axis. Value is %f." yVal
    | { x = 0.0; y = 0.0; z = zVal } -> printfn "Point is on the z-axis. Value is %f." zVal
    | { x = xVal; y = yVal; z = zVal } -> printfn "Point is at (%f, %f, %f)." xVal yVal zVal

evaluatePoint { x = 0.0
                y = 0.0
                z = 0.0 }
evaluatePoint { x = 100.0
                y = 0.0
                z = 0.0 }
evaluatePoint { x = 10.0
                y = 0.0
                z = -1.0 }

module Stock =
    open System
    open System.IO
    open System.Net

    type Tick =
        { Date: System.DateTime
          Open: float
          High: float
          Low: float
          Close: float
          Volume: float
          AdjClose: float }

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
            |> Seq.map(fun line ->
                   match line.Split(',') with
                   | [| date; ``open``; high; low; close; volume; adjClose |] ->
                       { Date = DateTime.Parse(date)
                         Open = float ``open``
                         High = float high
                         Low = float low
                         Close = float close
                         Volume = float volume
                         AdjClose = float adjClose }
                   | _ -> raise(System.InvalidOperationException()))
            |> Seq.toArray
        ticks

    let goog = load "GOOG" 300

    let findHighGrowth =
        goog
        |> Seq.pairwise
        |> Seq.sortBy(fun (t1, t2) -> -log(t2.AdjClose / t1.AdjClose))
        |> Seq.truncate 6
        |> Seq.iter(fun (t1, t2) -> printfn "%s" (t2.Date.ToShortDateString()))

    let movingAverage =
        goog
        |> Seq.windowed 15
        |> Seq.map(Array.averageBy(fun t -> t.AdjClose))
        |> Seq.toArray