import React from 'react'
import useCustomHook from './useCustomHook.js';

 function Home() {
  
    const [data]=useCustomHook("https://jsonplaceholder.typicode.com/todos"); 
    
   // const [data1]=useCustomHook("https://jsonplaceholder.typicode.com/todos");//Create Own Hook
  return (
  <>
 {data &&
 data.map((item)=>{
return <p key={item.id}>{item.title}</p>;
 })}
 {/* <h1>Second Data2</h1>
  {data1 &&
 data1.map((item)=>{
return <p key={item.id}>{item.title}</p>;
 })} */}
  </>
  )
}

export default Home;