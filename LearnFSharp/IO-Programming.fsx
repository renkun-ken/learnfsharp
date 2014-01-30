// I/O Programming
// Simple Read/Write Program
#r "System.Data.dll"
#r "System.Data.Linq.dll"

open Microsoft.FSharp.Linq
open System
open System.IO
open System.Linq

module SimpleReader = 
    let readText = File.ReadAllText(@"d:\data\book\products.txt")
    let readLines = File.ReadAllLines(@"d:\data\book\products.txt")
    
    let readItems = 
        File.ReadAllLines(@"d:\data\book\products.txt")
        |> Seq.map (fun line -> line.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries))
        |> Seq.map (function 
               | [| id; tp; count; cost |] -> (id, tp, int count, float cost)
               | _ -> failwith "Error")
        |> Seq.toArray
    
    type Tick = 
        { Date : DateTime
          Open : float
          High : float
          Low : float
          Close : float
          Volume : float
          Trade : float }
    
    let readTicks quote = 
        File.ReadAllLines(sprintf @"d:\data\book\%s.csv" quote)
        |> Seq.skip 1
        |> Seq.map (fun line -> line.Split([| ',' |], StringSplitOptions.RemoveEmptyEntries))
        |> Seq.map (function 
               | [| date; ``open``; high; low; close; volume; trade |] -> 
                   { Date = DateTime.Parse(date)
                     Open = float ``open``
                     High = float high
                     Low = float low
                     Close = float close
                     Volume = float volume
                     Trade = float trade }
               | _ -> failwith "Invalid input")
        |> Seq.toArray
    
    let sh000300 = readTicks "sh000300"
    let sh000905 = readTicks "sh000905"
    
    let combined = 
        use writer = new StreamWriter(@"d:\data\test\sh000300-sh000905.csv")
        writer.WriteLine("Date,sh000300,sh000905")
        query { 
            for t1 in sh000300 do
                for t2 in sh000905 do
                    where (t1.Date = t2.Date)
                    select (t1.Date, t1.Close, t2.Close)
        }
        |> Seq.iter 
               (fun (date, close1, close2) -> 
               writer.WriteLine(String.Join(",", date.ToShortDateString(), close1, close2)))

module SimpleWriter = 
    let writeString (str : string) = 
        use writer = new StreamWriter(@"d:\data\test\writer.txt")
        writer.Write(str)
        writer.Close()
    
    let writeStrings (strings : string list) = 
        strings |> List.iteri (fun i str -> 
                       use writer = new StreamWriter(sprintf @"d:\data\test\loop\string%d.txt" i)
                       writer.Write(str)
                       writer.Close())
    
    let writeNumbers n = 
        use writer = new StreamWriter(@"d:\data\test\output.txt")
        seq { 1..n } |> Seq.iter (fun x -> writer.WriteLine(x))
        writer.Close()
