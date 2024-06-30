import React from "react";
import { Grid,Paper } from "@mui/material";
import PropTypes from 'prop-types';
import { baseUrl } from "../utils/consts";

const GalleryGrid = ({images}) => {
    return (
        <Grid container spacing={2}>
            {images ? (images.map((image) =>(
                <Grid item xs ={12} sm={6} md={4} lg={3} key={image.id}>
                    <Paper elevation={2}>
                        <img src={baseUrl + image.filePath} alt="obraz"
                         style={{ width: '300px', height: '400px', objectFit: 'cover' }}  
                        />
                    </Paper>
                </Grid>
            ))) : (
                <p>Brak zdjec</p>
            )}
        </Grid>
    );
}

GalleryGrid.propTypes = {
    images: PropTypes.array.isRequired,
  };

export default GalleryGrid;