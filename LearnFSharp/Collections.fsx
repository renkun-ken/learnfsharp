// Collection Types
// http://msdn.microsoft.com/en-us/library/hh967652.aspx
open System
open System.Collections.Generic

// System.Collections.Generic.List<a'>
let list1 = List<int>(10)

for i in 1..10 do
    list1.Add(i)

// Dictionary
let dictionary = 
    let dict1 = 
        dict [ ("Ken", 100)
               ("James", 120)
               ("Jenny", 95) ]
    dict1.["Ken"]

// Map
let map = 
    let map1 = 
        Map [ ("Ken", 100)
              ("James", 120)
              ("Jenny", 95) ]
    
    let socre1 = map1.["Ken"]
    map1
    |> Map.add "Daisy" 150
    |> Map.filter(fun key value -> value <= 100)

// Set
let set = 
    let set1 = Set [ 1..100 ]
    let set2 = Set [ 5..95 ]
    Set.difference set1 set2

open System
open System.Linq
open Microsoft.FSharp.Linq

let str0 = """
    If the person experiences an increase in wealth, he/she will choose to increase (or keep unchanged, or decrease) the number of dollars of the risky asset held in the portfolio if absolute risk aversion is decreasing (or constant, or increasing). Thus economists avoid using utility functions, such as the quadratic, which exhibit increasing absolute risk aversion, because they have an unrealistic behavioral implication.
    """

let str = str0.ToLower()

let chars1 = 
    query { 
        for c in [ 'a'..'z' ] do
            let n = str.Count(fun x -> x = c)
            select(c, n)
    }
    |> Seq.toList

let chars2 = 
    let count c = str.Count(fun x -> x = c)
    [ 'a'..'z' ] |> List.map(fun c -> (c, count c))

let chars3 = [ 'a'..'z' ] |> List.map(fun c -> (c, str.Count(fun x -> x = c)))
