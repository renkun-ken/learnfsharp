// Lists
// A list in F# is an ordered, immutable series of elements of the same type.
// http://msdn.microsoft.com/en-us/library/dd233224.aspx
let list1 = [ 1; 2; 3 ]
let list2 = [ 1; 2; 3 ]
let list3 = [ 1..10 ]

let list4 = 
    [ for i in 1..10 do
          yield i * i ]

let list5 = 
    [ for i in 1..10 -> i * i ]

let emptyList = []
let list6 = 100 :: list1
let list7 = list2 @ list3

let reportList(list: int list) = 
    printfn "list.IsEmpty is %b" (list.IsEmpty)
    printfn "list.Length is %d" (list.Length)
    printfn "list.Head is %d" (list.Head)
    printfn "list.Tail.Head is %d" (list.Tail.Head)
    printfn "list.Tail.Tail.Head is %d" (list.Tail.Tail.Head)
    printfn "list.Item(1) is %d" (list.Item(1))

let rec sum list = 
    match list with
    | head :: tail -> head + sum tail
    | [] -> 0

let isPrimeMultipleTest n x = x = n || x % n <> 0

let rec removeAllMultiples listn listx = 
    match listn with
    | head :: tail -> 
        removeAllMultiples tail (List.filter (isPrimeMultipleTest head) listx)
    | [] -> listx

let getPrimesUpTo n = 
    let max = int(sqrt(float n))
    removeAllMultiples [ 2..max ] [ 2..n ]

let contains1 number list = List.exists (fun n -> n = number) list
let contains2 number list = list |> List.exists(fun n -> n = number)

let contains3 = 
    let list1 = [ 1..5 ]
    let list2 = [ 5..-1..1 ]
    List.exists2 (fun n1 n2 -> n1 = n2) list1 list2

let listForAll = 
    let list1 = [ 0; 0; 0; 0; 0; 0; 0; 0 ]
    let list2 = [ 0; 0; 1; 0; 0; 0; 0; 0 ]
    let allZeros = List.forall (fun n -> n = 0) list1
    let allZeros2 = list2 |> List.forall(fun n -> n = 0)
    do printfn "%b, %b" allZeros allZeros2

let listEqual = 
    let list1 = [ 0..1000 ]
    let list2 = [ 0..1000 ]
    List.forall2 (fun n1 n2 -> n1 = n2) list1 list2

let listSort = 
    let list = [ 1; 4; -2; -5; -8; 10; 2 ]
    List.sort list

let listSortBy = 
    let list = 
        [ (1, 0)
          (2, 3)
          (3, -1)
          (4, -2)
          (5, -9)
          (6, 4)
          (7, 6)
          (8, 3) ]
    List.sortBy (fun (n, x) -> x) list

type Widget = 
    { ID: int
      Rev: int }

let compareWidgets = 
    let compareWidgets widget1 widget2 = 
        if widget1.ID < widget2.ID then -1
        else if widget1.ID > widget2.ID then 1
        else if widget1.Rev < widget2.Rev then -1
        else if widget1.Rev > widget2.Rev then 1
        else 0
    
    let listToCompare = 
        [ { ID = 92
            Rev = 1 }
          { ID = 110
            Rev = 1 }
          { ID = 100
            Rev = 5 }
          { ID = 100
            Rev = 2 }
          { ID = 92
            Rev = 1 } ]
    
    let sortedWidgetList = List.sortWith compareWidgets listToCompare
    printfn "%A" sortedWidgetList

let listFind = 
    let isDivisibleBy number n = n % number = 0
    let result = [ 1..100 ] |> List.find(isDivisibleBy 5)
    printfn "%d" result

let listPick = 
    let list = 
        [ ("a", 1)
          ("b", 2)
          ("c", 3) ]
    
    let resultPick = 
        list |> List.pick(fun item -> 
                    match item with
                    | (value, 2) -> Some value
                    | _ -> None)
    
    do printfn "%s" resultPick

let listTryFind = 
    let list = [ 1; 3; 7; 9; 11; 13; 15; 19; 22; 29; 36 ]
    let isEven x = x % 2 = 0
    match List.tryFind isEven list with
    | Some value -> printfn "%d" value
    | None -> printfn "None"
    match List.tryFindIndex isEven list with
    | Some value -> printfn "pos: %d" value
    | None -> printfn "None"

let listSum = 
    let sum1 = List.sum [ 1..10 ]
    let sum2 = List.sumBy (fun n -> n * 2) [ 1..10 ]
    printfn "%d,%d" sum1 sum2

let listAverage = 
    let avg1 = List.average [ 1.0..100.0 ]
    let avg2 = List.averageBy (fun n -> n * 2.0) [ 1.0..20.0 ]
    
    let list1 = 
        [ (1, 2)
          (2, 3)
          (3, 5) ]
    
    let avg3 = List.averageBy (fun (n, x) -> float x) list1
    printfn "%f,%f,%f" avg1 avg2 avg3

let listZip = 
    let list1 = [ 1; 2; 3 ]
    let list2 = [ -1; -2; -3 ]
    let listZip = List.zip list1 list2
    printfn "%A" listZip
    let list3 = [ 0; 0; 0 ]
    let listZip3 = List.zip3 list1 list2 list3
    printfn "%A" listZip3

let listUnzip = 
    let lists = 
        List.unzip [ (1, 2)
                     (3, 4) ]
    printfn "%A" lists
    printfn "%A %A" (fst lists) (snd lists)
    let listsUnzip3 = 
        List.unzip3 [ (1, 2, 3)
                      (4, 5, 6) ]
    printfn "%A" listsUnzip3

let listIter = 
    let list1 = [ 1; 2; 3 ]
    let list2 = [ 4; 5; 6 ]
    List.iter (fun x -> printfn "iter: %d" x) list1
    List.iteri (fun i x -> printfn "iter: (%d) %d" i x) list1
    List.iter2 (fun x y -> printfn "iter: %d %d" x y) list1 list2
    List.iteri2 (fun i x y -> printfn "iter: (%d) %d %d" i x y) list1 list2

let listMap = 
    let list1 = [ 1; 2; 3 ]
    let list2 = [ 4; 5; 6 ]
    let list3 = [ 7; 8; 9 ]
    let listmap1 = List.map (fun x -> x + 1) list1
    let listmapi = List.mapi (fun i x -> i * x) list1
    let listmap2 = List.map2 (fun x y -> x + y) list1 list2
    let listmapi2 = List.mapi2 (fun i x y -> i * (x + y)) list1 list2
    let listmap3 = List.map3 (fun x y z -> x + y + z) list1 list2 list3
    printfn "%A" listmap1
    printfn "%A" listmapi
    printfn "%A" listmap2
    printfn "%A" listmapi2
    printfn "%A" listmap3

let listCollect = 
    let list1 = [ 1; 2; 3; 4 ]
    
    let clist1 = 
        list1 |> List.collect(fun x -> 
                     [ for i in 1..3 -> x * i ])
    printfn "%A" clist1
    let list2 = 
        [ ("A", 5)
          ("B", 8)
          ("C", 15) ]
    
    let clist2 = 
        list2 |> List.collect(fun (id, n) -> 
                     [ for i in 1..n -> (id, 1) ])
    
    printfn "%A" clist2

let listFilter = 
    let list = [ 1..200 ]
    List.filter (fun x -> x % 2 = 0) list

let listFilter2 = 
    let words = [ "and"; "Rome"; "Bob"; "apple"; "zebra" ]
    let isCapital(str: string) = System.Char.IsUpper str.[0]
    words
    |> List.filter isCapital
    |> List.map(fun word -> word + "'s")

let listChoose = 
    let words = [ "Rome"; "Bob"; "apple"; "zebra"; "appreciation" ]
    let isCapital(str: string) = System.Char.IsUpper str.[0]
    let isLong(str: string) = str.Length >= 8
    words |> List.choose(fun word -> 
                 match word with
                 | word when isCapital word -> Some(word + "'s")
                 | word when isLong word -> Some("some " + word)
                 | word -> Some("the " + word)
                 | _ -> None)

let listAppend = 
    let list1 = [ 1..3 ]
    let list2 = [ 4..10 ]
    List.append list1 list2

let listConcat = 
    List.concat [ [ 1..10 ]
                  [ 2..30 ]
                  [ 5..15 ]
                  [ 10..-2..-10 ] ]

let listFold = 
    let list1 = [ 1..100 ]
    let listfold1 = List.fold (fun acc n -> acc + n) 0 list1
    printfn "%A" listfold1
    let list2 = [ 'a'..'z' ]
    let listfold2 = List.fold (fun acc c -> acc + string c) "" list2
    printfn "%A" listfold2

let listReduce = 
    let list = [ 1..10 ]
    try 
        List.reduce (fun acc n -> acc + n) list
    with :? System.ArgumentException as exc -> 0
