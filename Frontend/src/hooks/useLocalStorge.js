import {useEffect, useState} from "react";

export default function useLocalStorge(key){
    const [value,setValue] = useState(() => {
        const  initValue = JSON.parse(localStorage.getItem(key))
        return initValue ? initValue : [];
    });

    useEffect(() => {
        localStorage.setItem(key, JSON.stringify(value));
    }, [value, key]);
    
    return [value,setValue];
}