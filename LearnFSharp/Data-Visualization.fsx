// Data visualization
// Reference: FSharp.Charting
// http://fsharp.github.io/FSharp.Charting/fsharpcharting.html
#load "../packages/FSharp.Charting.0.90.5/FSharp.Charting.fsx"

open System
open System.IO
open System.Linq
open Microsoft.FSharp.Linq
open FSharp.Charting

module lineChart = 
    Chart.Line [ for x in 1.0..100.0 -> (x, x ** 2.0) ]
    Chart.Line [ for i in 0.0..0.02..2.0 * Math.PI -> (sin i, cos i * sin i) ]
    
    let curvyData = 
        seq { 
            for i in 0.0..0.02..2.0 * Math.PI -> (sin i, cos i * sin(i / 2.0))
        }
    
    curvyData |> Chart.Point
    
    let rnd = Random()
    let rand() = rnd.NextDouble()
    
    let randomPoints = 
        [ for i in 0..1000 -> rand(), rand() ]
    
    Chart.Point randomPoints
    
    let highData = 
        seq { 
            for x in 1.0..100.0 -> (x, 3000.0 + x ** 2.0)
        }
    
    Chart.Line(highData, Name = "Rates").WithYAxis(Min = 2000.0)
         .WithXAxis(Log = true)
    
    let futureDate numDays = DateTime.Today.AddDays(float numDays)
    
    let expectedIncome = 
        [ for x in 1..100 -> 
              (futureDate x, 1000.0 + rand() * 100.0 * exp(float x / 40.0)) ]
    
    let expectedExpenses = 
        [ for x in 1..100 -> 
              (futureDate x, rand() * 500.0 * sin(float x / 50.0)) ]
    
    let computedProfit = 
        (expectedIncome, expectedExpenses) 
        ||> List.map2(fun (d1, i) (d2, e) -> (d1, i - e))
    
    Chart.Combine([ Chart.Line(expectedIncome, Name = "Income")
                    Chart.Line(expectedExpenses, Name = "Expenses")
                    Chart.Line(computedProfit, Name = "Profit") ])
         .WithTitle(Text = "Combined Lines", InsideArea = false)

// Random Walk
// Brownian Motion
// Geometric Brownian Motion
module barChart = 
    Chart.Bar [ 0..10 ]
    
    let countryData = 
        [ "Africa", 1033043
          "Asia", 4166741
          "Europe", 732759
          "South America", 588649
          "North America", 351659
          "Oceania", 35838 ]
    
    Chart.Bar countryData
    Chart.Column countryData

module pieChart = 
    let electionData = 
        [ "Conservative", 306
          "Labour", 258
          "Liberal Democrat", 57 ]
    
    Chart.Pie electionData
    Chart.Doughnut electionData

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
            let lines = 
                data.Split([| '\n' |], StringSplitOptions.RemoveEmptyEntries)
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
            |> Array.rev
        ticks
    
    let goog = 
        load "GOOG" 80
        |> Array.map
               (fun tick -> 
               (tick.Date.ToShortDateString(), tick.High, tick.Low, tick.Open, 
                tick.Close))
        |> Chart.Candlestick
        |> Chart.WithYAxis(Min = 800.0, Max = 1120.0)

module boxPlot = 
    let date n = DateTime.Today.AddDays(float n).ToShortDateString()
    let rnd = new System.Random()
    
    let threeSyntheticDataSets = 
        [ (date 0, 
           [| for i in 0..20 -> float(rnd.Next 20) |])
          (date 1, 
           [| for i in 0..20 -> float(rnd.Next 15 + 2) |])
          (date 2, 
           [| for i in 0..20 -> float(rnd.Next 10 + 5) |]) ]
    
    Chart.BoxPlotFromData
        (threeSyntheticDataSets, ShowUnusualValues = true, ShowMedian = true, 
         ShowAverage = true, WhiskerPercentile = 10)
