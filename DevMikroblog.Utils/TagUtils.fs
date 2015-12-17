namespace Tools
open System.Text.RegularExpressions
module TagUtils =
    type Parsers() =
        static member TagParser (text:string) (pattern:Regex):string[] = 
            let result = pattern.Matches(text)
            [for x in result -> x.Value.ToLower()] |> List.toArray