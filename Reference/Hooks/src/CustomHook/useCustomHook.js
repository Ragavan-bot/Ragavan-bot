import { useEffect, useState } from 'react';

 const useCustomHook = (url)=> {
    const [data,setdata]=useState(null);
    useEffect(()=>{
fetch(url)
.then((res)=>res.json())
.then((data)=>setdata(data))
    },[url]);
    return [data];
}

export default useCustomHook;