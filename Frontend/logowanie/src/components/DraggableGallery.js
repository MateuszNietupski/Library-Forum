import React, { useState } from "react";
import { Grid, Paper,Button } from "@mui/material";
import { Draggable, Droppable, DragDropContext } from 'react-beautiful-dnd';
import PropTypes from 'prop-types';
import { baseUrl } from "../utils/consts";
import axios from "axios";

const DraggableGallery = ({images}) => {
    const [galleryOrder, setGalleryOrder] = useState(images);

    const handleDragEnd = (result) => {
        if (!result.destination) return;

        const updatedOrder = Array.from(galleryOrder);
        const [movedImage] = updatedOrder.splice(result.source.index, 1);
        updatedOrder.splice(result.destination.index, 0, movedImage);

        setGalleryOrder(updatedOrder);
    };
    
    const handleSaveButtonClik = () => {
        
        
    }
    
    return (
        <div>
            <DragDropContext onDragEnd={handleDragEnd}>
                <Droppable droppableId="gallery">
                    {(provided) => (
                        <Grid container spacing={2} ref={provided.innerRef} {...provided.droppableProps}>
                            {galleryOrder.map((image, index) => (
                                <Draggable key={image.id} draggableId={image.id} index={index}>
                                    {(provided) => (
                                        <Grid item xs={12} sm={6} md={4} lg={3} ref={provided.innerRef} {...provided.draggableProps} {...provided.dragHandleProps}>
                                            <Paper elevation={2}>
                                                <img
                                                    src={baseUrl + image.filePath}
                                                    alt="obraz"
                                                    style={{ width: '300px', height: '400px', objectFit: 'cover' }}
                                                />
                                            </Paper>
                                        </Grid>
                                    )}
                                </Draggable>
                            ))}
                            {provided.placeholder}
                        </Grid>
                    )}
                </Droppable>
            </DragDropContext>
            <Button onClick={handleSaveButtonClik}>
                Zapisz
            </Button>
        </div>
        
    );

};

DraggableGallery.propTypes = {
    images: PropTypes.array.isRequired,
  };

export default DraggableGallery;