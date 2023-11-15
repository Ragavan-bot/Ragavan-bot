import React, { useEffect, useReducer } from 'react'

const initialState={
isRunning :false,
value :"",
elementavalue :null, // set Default Value
time:0
};
function UseReducer() {
 const [state,dispatch]=useReducer(reducer,initialState);//use the UseReducer for multiple usestate 
 // reducer : Pass two arguments action & state
 //useReducer:pass two agruments state& dispatch

 //timer
useEffect(()=>{
    let timer=0;
    if(!state.isRunning){
        return;
    }
    timer = setInterval(()=>dispatch({type:"tick"},{types:null}),1000);
    return ()=>{
        clearInterval(timer);
        timer=0;
    }
},[state.isRunning]);


//get e of Element data From Button
const Oncliker=(e)=>{debugger
var data =e.timeStamp;

dispatch({type:"BtnClick",payload:data});
console.log("data",data)
}
 return (
    <div>Second :
        <button onClick={()=>{dispatch({type:"start"})}}>Start</button>
        <button onClick={()=>{dispatch({type:"stop"})}}>Stop</button>
        <button onClick={()=>{dispatch({type:"reset"})}}>Reset</button><br></br>
        {state.time}
        <br></br>
        <button onClick={()=>{dispatch({type:"Clicker"})}}>Click</button>
        <p>{state.value}</p>
        <button onClick={Oncliker}>Element</button>
        <p>{state.elementavalue}</p>
    </div>
  )
}
function reducer(state,action){
    switch (action.type){
        case "start":
            return {...state,isRunning:true};
        case "stop":
            return {...state,isRunning:false};
       case "reset":
        return {...state,time:0}

        case "Clicker": 
        const Name="Welcome";
           return {...state, value:Name}


          case "BtnClick":debugger
            return {...state,elementavalue:action.payload}
        case "tick":
           return {...state,time:state.time +1};
    default :
    throw new Error();
        }
}

export default UseReducer;