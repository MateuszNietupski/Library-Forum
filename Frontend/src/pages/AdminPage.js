import DraggableGallery from "../components/DraggableGallery";
import React, {useEffect, useState} from "react";
import axios from "axios";
import {ENDPOINTS} from "../utils/consts";
import axiosAuth from "../utils/authInstance";
import ConfirmLoans from "../components/ConfirmLoans";



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
            <ConfirmLoans/>
        </>
    );

}

export default AdminPage;