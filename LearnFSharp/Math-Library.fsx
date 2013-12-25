// Math.NET Library
#r "../packages/MathNet.Numerics.2.6.2/lib/net40/MathNet.Numerics.dll"
#r "../packages/MathNet.Numerics.FSharp.2.6.0/lib/net40/MathNet.Numerics.FSharp.dll"
#load "../packages/FSharp.Charting.0.90.5/FSharp.Charting.fsx"

open MathNet.Numerics
open FSharp.Charting

module RandomNumbers = 
    open MathNet.Numerics.Random
    
    let rnd = Random.mersenneTwister()
    
    let sample = 
        seq { 
            for i in 1..1000 -> rnd.NextDouble()
        }
        |> Seq.toArray

module Probability = 
    open MathNet.Numerics.Distributions
    
    let normal = Normal(0.0, 2.0)
    let nmean = normal.Mean
    let nvar = normal.Variance
    let ncum1 = normal.CumulativeDistribution(4.0)
    let nentropy = normal.Entropy
    
    let nsample = 
        normal.Samples()
        |> Seq.take 1000
        |> Chart.Point
    
    let gamma = Gamma(2.0, 1.5)
    let gmean = gamma.Mean
    let gvar = gamma.Variance
    let gcum1 = gamma.CumulativeDistribution(4.0)
    let gentropy = gamma.Entropy
    
    let gsample = 
        gamma.Samples()
        |> Seq.take 1000
        |> Chart.Point

module Staitstics = 
    open MathNet.Numerics.Distributions
    open MathNet.Numerics.Statistics
    
    let normal = Normal(0.0, 2.0)
    let nsample = normal.Samples() |> Seq.take 1000
    let nstats = DescriptiveStatistics(nsample)
    let nmax0 = nstats.Maximum
    let nmin0 = nstats.Minimum
    let nmean = nstats.Mean
    let nvar = nstats.Variance
    let nstddev = nstats.StandardDeviation
    let nkurtosis = nstats.Kurtosis
    let nskewness = nstats.Skewness
    // Extension methods
    let gamma = Gamma(2.0, 1.5)
    let gsample = gamma.Samples() |> Seq.take 1000
    let gmax = gsample.Maximum()
    let gmin = gsample.Minimum()
    let gmean = gsample.Mean()
    let gmedian = gsample.Median()
    let gvar = gsample.Variance()
    let gstddev = gsample.StandardDeviation()
    let g25q = gsample.Quantile(0.25)
    let g75q = gsample.Quantile(0.75)
    let gqs = 
        [| 0.05; 0.25; 0.50; 0.75; 0.95 |] 
        |> Array.map(fun q -> gsample.Quantile(q))
    let gpvar = gsample.PopulationVariance()
    let gpstddev = gsample.PopulationStandardDeviation()
    // Histogram
    let hist = Histogram(nsample, 10)
    let buck1 = hist.[0]
    // Correlation
    let corr = Correlation.Pearson(nsample, gsample)

module LinearAlgebra = 
    open MathNet.Numerics.LinearAlgebra.Double
    
    let x = DenseVector.ofSeq(seq { 1.0..5.0 })
    let A = DenseMatrix.create 5 5 2.0
    let y = A * x
