import React, { createContext, useState } from 'react'
import UseContextChild from './UseContextChild.js';
import UseContextChildTwo from './UseContextChildTwo.js';
import UseEffectComponent from '../UseEffect/UseEffectComponent.js';
import UseRefComponent from '../UseRef/UseRefComponent.js';

export const userContext=createContext();  //Give a createContext() from one to more then component
export default function UseContextparent() {
    const [user,setuser]=useState("React");  
    const [name, setName]=useState("Vignesh");
  return (
    // pass multiple values
    <div>
    <userContext.Provider value={{user:[user, setuser], name: [name, setName]} }> 
    <UseContextChild />
    <UseContextChildTwo />
    
    </userContext.Provider>
    
    <UseEffectComponent />
   <UseRefComponent />
    </div>
  )

}
