// Classes
// Classes are types that represent objects that can have properties, methods, and events.
// http://msdn.microsoft.com/en-us/library/dd233205.aspx
// Constructors
type MyClass1(x : int, y : int) = 
    do printfn "%d %d" x y
    new() = MyClass1(0, 0)

let myClass1 = MyClass1(10, 12)

// Self Identifiers
type MyClass2(dataIn) as self = 
    let data = dataIn
    do self.PrintMessage()
    member this.PrintMessage() = printf "Creating MyClass2 with Data %d" data

let myClass2 = MyClass2(10)

// Generic Type Parameters
type MyGenericClass<'a>(x : 'a) = 
    do printfn "%A" x

let g1 = 
    MyGenericClass(seq { 
                       for i in 1..10 -> (i, i * i)
                   })

// Inheritance
// http://msdn.microsoft.com/en-us/library/dd233207.aspx
type IPrintable = 
    abstract Print : unit -> unit

type SomeClass1(x : int, y : float) = 
    interface IPrintable with
        member this.Print() = printfn "%d %f" x y

let x1 = new SomeClass1(1, 2.0)

(x1 :> IPrintable).Print()

type SomeClass2(x : int, y : float) = 
    member this.Print() = (this :> IPrintable).Print()
    interface IPrintable with
        member this.Print() = printfn "%d %f" x y

let x2 = new SomeClass2(1, 2.0)

x2.Print()

let makePrintable (x : int, y : float) = 
    { new IPrintable with
          member this.Print() = printfn "%d %f" x y }

let x3 = makePrintable (1, 2.0)

x3.Print()

type Interface1 = 
    abstract Method1 : int -> int

type Interface2 = 
    abstract Method2 : int -> int

type Interface3 = 
    inherit Interface1
    inherit Interface2
    abstract Method3 : int -> int

type MyClass() = 
    interface Interface3 with
        member this.Method1(n) = 2 * n
        member this.Method2(n) = n + 100
        member this.Method3(n) = n / 10

// Mutually Recursive Types
module Files = 
    open System.IO
    
    type Folder(pathIn : string) = 
        let path = pathIn
        let filenameArray : string array = Directory.GetFiles(path)
        member this.FileArray = Array.map (fun elem -> new File(elem, this)) filenameArray
    
    and File(filename : string, containingFolder : Folder) = 
        member this.Name = filename
        member this.ContainingFolder = containingFolder
    
    let folder1 = new Folder(".")
    
    for file in folder1.FileArray do
        printfn "%s" file.Name

// Add application examples
module Person = 
    open System
    
    type Gender = 
        | Male
        | Female
    
    type Person = 
        { Name : string
          Gender : Gender
          BirthDate : DateTime
          ID : string }
    
    type Student = 
        { StudentID : string }
