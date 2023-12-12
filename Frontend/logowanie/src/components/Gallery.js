import React, {useState,useEffect} from "react";
import axios from "axios";
import GalleryGrid from "./GalleryGrid";
import { ENDPOINTS } from "../utils/consts";
import DraggableGallery from "./DraggableGallery";

const Gallery = () => {
    const [images,setImages] = useState([]);
    const [isLoading,setIsLoading] = useState(true);
    useEffect(() => {
        const fetchData = async () => {
        try {
        const response = await axios.get(ENDPOINTS.GETGALLERY)
        setImages(response.data);
        setIsLoading(false);
        } catch (error) {
            console.error(error);
        }
    };

    fetchData();

    }, []);

    return (
        <div>
            <h1>Galeria</h1>
            {isLoading ? null : <DraggableGallery images={images} />} 
            
        </div>
    );
};

export default Gallery;
