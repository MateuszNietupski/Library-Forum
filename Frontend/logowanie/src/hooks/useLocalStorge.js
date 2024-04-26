import {useEffect, useState} from "react";

export default function useLocalStorge(key){
    const [value,setValue] = useState(() => {
        return JSON.parse(localStorage.getItem(key));
    });

    useEffect(() => {
        localStorage.setItem(key, JSON.stringify(value));
    }, [value, key]);
    
    return [value,setValue];
}