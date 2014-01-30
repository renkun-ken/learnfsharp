// Functional Programming
module TRD_Co = 
    // Load Data
    open Microsoft.FSharp.Linq
    open System
    open System.IO
    open System.Linq
    
    type StockInfo = 
        { StockCode : string
          StockName : string
          IndustryCode : string
          IndustryName : string
          NIndustryCode : string
          NIndustryName : string
          EstablishDate : DateTime
          ListDate : DateTime
          MarketType : int }
    
    let data = 
        File.ReadAllLines(@"d:\data\csmar\trd_co.txt", Text.Encoding.UTF8)
        |> Seq.skip 1
        |> Seq.map (fun line -> line.Split([| '\t' |], StringSplitOptions.RemoveEmptyEntries))
        |> Seq.choose (function 
               | [| stkcd; stknme; indcd; indnme; nindcd; nindnme; estbdt; listdt; markettype |] -> 
                   let edate1, edate = DateTime.TryParse(estbdt)
                   let ldate1, ldate = DateTime.TryParse(listdt)
                   Some({ StockCode = stkcd
                          StockName = stknme
                          IndustryCode = indcd
                          IndustryName = indnme
                          NIndustryCode = nindcd
                          NIndustryName = nindnme
                          EstablishDate = edate
                          ListDate = ldate
                          MarketType = int markettype })
               | _ -> None)
        |> Seq.toList
    
    let analysis1 = 
        data
        |> Seq.filter (fun item -> item.ListDate >= DateTime(2006, 1, 1))
        |> Seq.groupBy (fun item -> item.IndustryCode)
        |> Seq.map (fun (indcd, items) -> (indcd, (Seq.toArray items).Length))
        |> Seq.toList
    
    let analysis2 = 
        query { 
            for item in data do
                where (item.ListDate >= DateTime(2006, 1, 1))
                groupBy item.IndustryCode into g
                let count = 
                    query { 
                        for item in g do
                            count
                    }
                // Less Expensive
                select (g.Key, count)
        }
        |> Seq.toList
    
    let analysis3 = 
        query { 
            for item in data do
                groupBy item.IndustryCode into g
                let avg = 
                    query { 
                        for item in g do
                            let life = item.ListDate - item.EstablishDate
                            averageBy life.TotalDays
                    }
                select (g.Key, avg)
        }
        |> Seq.toList
