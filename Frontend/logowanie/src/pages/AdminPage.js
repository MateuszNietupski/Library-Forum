import DraggableGallery from "../components/DraggableGallery";
import React, {useEffect, useState} from "react";
import axios from "axios";
import {ENDPOINTS} from "../utils/consts";
import axiosAuth from "../utils/authInstance";



const AdminPage = () => {
    const [images,setImages] = useState([])
    const [isLoading,setIsLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axiosAuth.get(ENDPOINTS.GETGALLERY)
                setImages(response.data);
                setIsLoading(false);
            } catch (error) {
                console.error(error);
            }
        };
        fetchData();

    }, []);
    
    return (
        <>
            {isLoading ? null : <DraggableGallery images={images} />}
        </>
    );

}

export default AdminPage;