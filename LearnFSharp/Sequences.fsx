// Sequences
// A sequence is a logical series of elements all of one type.
// http://msdn.microsoft.com/en-us/library/dd233209.aspx
let seq1 = seq { 0..100 }
let seq2 = seq { 0..10..100 }

let seq3 =
    seq {
        for i in 1..10 do
            yield i * i
    }

let seq4 =
    seq {
        for i in 1..10 -> i * i
    }

let grid =
    let (height, width) = (10, 10)
    seq {
        for row in 0..width - 1 do
            for col in 0..height - 1 do
                yield (row, col, row * width + col)
    }

let seq5 =
    seq {
        for n in 1..100 do
            if n % 3 = 0 then yield n
    }

let seq6 =
    seq {
        yield 1
        yield 2
        yield! seq { 1..10 }
    }

let randomWalk =
    let rnd = System.Random()

    /// An infinite sequence which is a random walk
    //  Use yield! to return each element of a subsequence, similar to IEnumerable.SelectMany.
    let rec randomWalk x =
        seq {
            yield x
            yield! randomWalk(x + rnd.NextDouble() - 0.5)
        }

    let rec randomWalk2 x0 x1 =
        seq {
            yield x0
            yield x1
            yield! randomWalk2 (0.6 * x0 + 0.4 * x1 + rnd.NextDouble() - 0.5)
                       (0.8 * x0 + 0.2 * x1 + 0.6 * rnd.NextDouble() - 0.3)
        }

    let path1 =
        randomWalk 10.0
        |> Seq.take 500
        |> Seq.toList

    let path2 =
        randomWalk2 10.0 11.2
        |> Seq.take 30
        |> Seq.toList

    0

let multiplicationTable =
    seq {
        for i in 1..9 do
            for j in 1..9 do
                yield (i, j, i * j)
    }

let seqInit =
    let seq0 = Seq.init 5 (fun n -> n * 10)
    let print n = printfn "Printing %d" n
    Seq.iter print seq0

let seqRandom n =
    let rnd = System.Random()
    let seq0 = Seq.init n (fun n -> rnd.NextDouble())
    let print x = printfn "%f" x
    Seq.iter print seq0
    printfn "(end)"

let seqInfRandom n =
    let rnd = System.Random()

    let rec random seed =
        seq {
            yield rnd.NextDouble()
            yield! random seed
        }

    let print x = printfn "%f" x
    random 0
    |> Seq.take n
    |> Seq.toList

let seqInfinite =
    Seq.initInfinite(fun index ->
        let n = float(index + 1)
        1.0 / (n * n * (if ((index + 1) % 2 = 0) then 1.0
                        else -1.0)))

let seqFromArray1 = [| 1..10 |] :> seq<int>
let seqFromArray2 = [| 1..10 |] |> Seq.ofArray

let seqFromArrayList =
    let mutable arrayList1 = System.Collections.ArrayList(10)
    for i in 1..10 do
        arrayList1.Add(10) |> ignore
    let seqCast: seq<int> = Seq.cast arrayList1
    seqCast

let seqTruncate =
    let seq0 = seq { 1..10 }

    let seq1 =
        seq0
        |> Seq.take 5
        |> Seq.toList

    let seq2 =
        seq0
        |> Seq.take 15
        |> Seq.toList // Error!

    let seq3 =
        seq0
        |> Seq.truncate 5
        |> Seq.toList

    let seq4 =
        seq0
        |> Seq.truncate 15
        |> Seq.toList

    0

let seqTakeWhile =
    let seq0 =
        seq {
            for i in 1..100 -> i * 2 - i % 3
        }
    seq0
    |> Seq.takeWhile(fun x -> x <= 30)
    |> Seq.toArray

let seqSkip =
    let seq0 =
        seq {
            for i in 1..100 -> i * 2 - i % 3
        }
    seq0
    |> Seq.skip 3
    |> Seq.toArray

let seqSkipWhile =
    let seq0 =
        seq {
            for i in 1..100 -> i * 2 - i % 3
        }
    seq0
    |> Seq.skipWhile(fun x -> x <= 30)
    |> Seq.toArray

let seqPairwise =
    let seq0 =
        seq {
            for i in 1..10 -> i * 2 - i % 3
        }
    seq0
    |> Seq.pairwise
    |> Seq.map(fun (x, y) -> log(float y / float x))
    |> Seq.toArray

let seqWindowed =
    let seq0 =
        seq {
            for i in 1..100 -> float(i * 2 - i % 3)
        }
    seq0
    |> Seq.windowed 5
    |> Seq.map Array.average
    |> Seq.toArray

let seqZip =
    let seq1 = seq { 1..26 }
    let seq2 = seq { 'a'..'z' }
    Seq.zip seq1 seq2

let seqUnzip =
    let seq0 =
        seq {
            for i in 1..10 -> (i, 2 * i - 1)
        }
    seq0
    |> Seq.toList
    |> List.unzip

let seqCount =
    let seq0 =
        seq {
            for i in 1..100 -> (i % 5, i)
        }
    seq0
    |> Seq.countBy(fun (x, y) -> x)
    |> Seq.toArray

let seqGroupBy =
    let seq0 = seq { 1..30 }
    seq0
    |> Seq.groupBy(fun x -> x % 5)
    |> Seq.map(fun (x, y) -> (x, Seq.toArray y))
    |> Seq.toArray

let seqDistinct =
    let seq0 =
        seq {
            for i in 1..100 -> i % 7
        }
    seq0
    |> Seq.distinct
    |> Seq.toArray

let seqDistinctBy =
    let seq0 = seq { 1..100 }
    seq0
    |> Seq.distinctBy(fun x -> x % 7)
    |> Seq.toArray

let seqCache =
    let seq0 =
        seq {
            for i in 1..10000 -> (i, i % 7)
        }
    seq0 |> Seq.cache