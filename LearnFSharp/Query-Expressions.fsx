// Query Expressions
// TODO: Change to local database
#r "System.Data.dll"
#r "System.Data.Linq.dll"
#r "FSharp.Data.TypeProviders.dll"
#r "System.Data.Services.Client.dll"

open System
open System.Linq
open System.Data.Linq.SqlClient
open System.Data.SqlClient
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

module NorthwinData =
    type Northwind = ODataService< "http://services.odata.org/Northwind/Northwind.svc" >

    let db = Northwind.GetDataContext()

    let query1 =
        query {
            for customer in db.Customers do
                select customer
        }

    query1 |> Seq.iter(fun customer -> printfn "Company: %s Contact: %s" customer.CompanyName customer.ContactName)

type schema = SqlDataConnection< "Data Source=(LocalDB)\\v11.0;Initial Catalog=Students;Integrated Security=True" >

let db = schema.GetDataContext()
let data = [ 1; 5; 7; 11; 18; 21 ]

let contains =
    query {
        for student in db.Student do
            select student.Age.Value
            contains 11
    }

let countOfStudents =
    query {
        for student in db.Student do
            select student
            count
    }

let last =
    query {
        for number in data do
            last
    }

let lastOrDefault =
    query {
        for number in data do
            where(number < 0)
            lastOrDefault
    }

let exactlyOne =
    query {
        for student in db.Student do
            where(student.StudentID = 1)
            select student
            exactlyOne
    }

let exactlyOneOrDefault =
    query {
        for student in db.Student do
            where(student.StudentID = 1)
            select student
            exactlyOneOrDefault
    }

let headOrDefault =
    query {
        for student in db.Student do
            select student
            headOrDefault
    }

let select =
    query {
        for student in db.Student do
            select student.Name
    }
    |> Seq.toArray

let where =
    query {
        for student in db.Student do
            where(student.StudentID > 4)
            select student
    }
    |> Seq.toArray

let minBy =
    query {
        for student in db.Student do
            minBy student.StudentID
    }

let maxBy =
    query {
        for student in db.Student do
            maxBy student.StudentID
    }

let sumBy =
    query {
        for student in db.Student do
            sumBy student.StudentID
    }

let sumByNullable =
    query {
        for student in db.Student do
            sumByNullable student.Age
    }

let minByNullable =
    query {
        for student in db.Student do
            minByNullable student.Age
    }

let maxByNullable =
    query {
        for student in db.Student do
            maxByNullable student.Age
    }

let averageBy =
    query {
        for student in db.Student do
            averageBy(float student.StudentID)
    }

let averageByNullable =
    query {
        for student in db.Student do
            averageByNullable(Nullable.float student.Age)
    }

let distinct =
    query {
        for student in db.Student do
            join selection in db.CourseSelection on (student.StudentID = selection.StudentID)
            select selection.Course.CourseName
            distinct
    }
    |> Seq.toArray

let exists =
    query {
        for student in db.Student do
            where(query {
                      for courseSelection in db.CourseSelection do
                          exists(courseSelection.StudentID = student.StudentID)
                  })
            select student
    }
    |> Seq.toArray

let exists2 =
    // Find students who have signed up at least three courses
    query {
        for student in db.Student do
            where(query {
                      for courseSelection in db.CourseSelection do
                          where(courseSelection.StudentID = student.StudentID)
                          count
                  } >= 3)
            select student.Name
    }
    |> Seq.toArray

let find =
    query {
        for student in db.Student do
            find(student.Name = "Abercrombie, Kim")
    }

let all =
    query {
        for student in db.Student do
            all(SqlMethods.Like(student.Name, "%,%"))
    }

let head =
    query {
        for student in db.Student do
            head
    }

let nth =
    query {
        for numbers in data do
            nth 3
    }

let skip =
    query {
        for student in db.Student do
            skip 1
    }

let skipWhile =
    query {
        for number in data do
            skipWhile(number < 3)
            select number
    }

let take =
    query {
        for student in db.Student do
            select student
            take 2
    }
    |> Seq.toArray

let takeWhile =
    query {
        for number in data do
            takeWhile(number < 10)
    }

let averageAgeByNumberOfCourses =
    query {
        for student in db.Student do
            groupBy student.CourseSelection.Count into g
            let avg =
                query {
                    for s in g do
                        where s.Age.HasValue
                        averageBy(float s.Age.Value)
                }
            select(g.Key, g.Count(), avg)
    }
    |> Seq.toArray

let groupBy =
    query {
        for student in db.Student do
            groupBy student.Age into g
            select(g.Key, g.Count())
    }
    |> Seq.toArray

let sortBy =
    query {
        for student in db.Student do
            sortBy student.Name
            select student
    }
    |> Seq.toArray

let sortByDescending =
    query {
        for student in db.Student do
            sortByDescending student.Name
            select student
    }
    |> Seq.toArray

let thenBy =
    query {
        for student in db.Student do
            where student.Age.HasValue
            sortBy student.Age.Value
            thenBy student.Name
            select student
    }
    |> Seq.toArray

let thenByDescending =
    query {
        for student in db.Student do
            where student.Age.HasValue
            sortBy student.Age.Value
            thenByDescending student.Name
            select student
    }
    |> Seq.toArray

let sortByNullable =
    query {
        for student in db.Student do
            sortByNullable student.Age
            select student
    }

let sortByNullableDescending =
    query {
        for student in db.Student do
            sortByNullableDescending student.Age
            select student
    }

let thenByNullable =
    query {
        for student in db.Student do
            sortBy student.Name
            thenByNullable student.Age
            select student
    }

let thenByNullableDescending =
    query {
        for student in db.Student do
            sortBy student.Name
            thenByNullableDescending student.Age
            select student
    }

let groupValBy =
    query {
        for student in db.Student do
            groupValBy student.Name student.Age into g
            select(g, g.Key, g.Count())
    }
    |> Seq.toArray

let join =
    query {
        for student in db.Student do
            join selection in db.CourseSelection on (student.StudentID = selection.StudentID)
            select(student, selection)
    }
    |> Seq.toArray

let groupJoin =
    query {
        for student in db.Student do
            groupJoin courseSelection in db.CourseSelection on (student.StudentID = courseSelection.StudentID) into g
            for courseSelection in g do
                join course in db.Course on (courseSelection.CourseID = course.CourseID)
                select(student.Name, course.CourseName)
    }
    |> Seq.toArray

let leftOuterJoin =
    query {
        for student in db.Student do
            leftOuterJoin selection in db.CourseSelection on (student.StudentID = selection.StudentID) into result
            for selection in result.DefaultIfEmpty() do
                select(student, selection)
    }
    |> Seq.toArray

let inQuery =
    let idQuery =
        query {
            for id in [ 1; 2; 5; 10 ] do
                select id
        }
    query {
        for student in db.Student do
            where(idQuery.Contains(student.StudentID))
            select student.Name
    }
    |> Seq.toArray

let LikeQuery =
    query {
        for student in db.Student do
            where(SqlMethods.Like(student.Name, "_e%"))
            select student
            take 2
    }
    |> Seq.toArray

let LikeQuery2 =
    query {
        for student in db.Student do
            where(SqlMethods.Like(student.Name, "[abc]%"))
            select student
    }
    |> Seq.toArray

let LikeQuery3 =
    query {
        for student in db.Student do
            where(SqlMethods.Like(student.Name, "[^abc]%"))
            select student
    }
    |> Seq.toArray

let BetweenQuery =
    query {
        for student in db.Student do
            where(student.Age ?>= 10 && student.Age ?< 15)
            select student
    }
    |> Seq.toArray

let QueryOperator =
    query {
        for student in db.Student do
            where(student.Age ?= 11 || student.Age ?= 12)
            sortByDescending student.Name
            select student.Name
            take 2
    }
    |> Seq.toArray

let CaseQuery =
    query {
        for student in db.Student do
            select(if student.Age ?>= 12 then "Senior"
                   else if student.Age ?< 12 then "Junior"
                   else "Unknown")
    }
    |> Seq.toArray

let multipleSelect =
    query {
        for student in db.Student do
            for course in db.Course do
                select(student.Name, course.CourseName)
    }
    |> Seq.toArray

let multipleSelect2 =
    let list1 = [ 'a'..'e' ]
    let list2 = [ 1..5 ]
    query {
        for a in list1 do
            for b in list2 do
                select(a, b)
    }
    |> Seq.toArray

let multipleJoin =
    query {
        for student in db.Student do
            join courseSelection in db.CourseSelection on (student.StudentID = courseSelection.StudentID)
            join course in db.Course on (courseSelection.CourseID = course.CourseID)
            select(student.Name, course.CourseName)
    }
    |> Seq.toArray