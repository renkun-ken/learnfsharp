// Financial Computing
// http://msdn.microsoft.com/en-us/library/vstudio/hh304363%28v=vs.100%29.aspx#yih
#r "../packages/MathNet.Numerics.2.6.2/lib/net40/MathNet.Numerics.dll"
#r "../packages/MathNet.Numerics.FSharp.2.6.0/lib/net40/MathNet.Numerics.FSharp.dll"
#load "../packages/FSharp.Charting.0.90.5/FSharp.Charting.fsx"

open MathNet.Numerics
open MathNet.Numerics.Distributions
open FSharp.Charting

// Random Walk
let normal = Normal(0.0, 1.0)

let rec randomWalk1 x0 =
    seq {
        yield x0
        yield! randomWalk1(x0 + normal.Sample())
    }

let rw1 =
    randomWalk1 10.0
    |> Seq.take 2000
    |> Chart.Line

let rw3 x0 length n =
    Chart.Combine(seq {
                      for i in 1..n do
                          yield randomWalk1 x0
                                |> Seq.take length
                                |> Chart.Line
                  })
    |> Chart.WithTitle(Text = (sprintf "%d random walks" n), InsideArea = false)

let rw3s = rw3 10.0 600 5