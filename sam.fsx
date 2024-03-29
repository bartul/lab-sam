type Model(counter: int, renderer: Model -> Model option) =
    let mutable counterValue = counter
    let render = renderer

    member this.Counter with get() = counterValue and private set(value) = counterValue <- value
    
    member this.Accept step = 
        this.Counter <- this.Counter + step
        render this

let startReactiveLoop (model : Model) = model.Accept 0

module Actions =
    let addByStep step (model : Model) = model.Accept step

module State =
    let nextAction (model : Model) =
        if model.Counter < 100 then 
            printfn "Current count is %i, adding 5" model.Counter 
            let newModel = Actions.addByStep 5 model
            newModel, true 
        else
            Some(model), false

    let render (model : Model) = 
        let newModel, hasNextAction = nextAction model
        if hasNextAction then
            newModel
        else
            printfn "Count at %i" model.Counter
            None
        
let model = Model(0, State.render)
startReactiveLoop model