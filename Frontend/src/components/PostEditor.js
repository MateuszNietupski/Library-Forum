import React, {useContext, useState} from 'react';
import { TextField, Button, Box } from '@mui/material';
import axiosAuth from "../utils/authInstance";
import {ENDPOINTS} from "../utils/consts";
import {useParams} from "react-router-dom";
import {AuthContext} from "../context/AuthProvider";

const PostEditor = ({onAddPostSubmit}) => {
    const [title, setNewTitle] = useState('');
    const [content, setNewContent] = useState('');
    const { user } = useContext( AuthContext );
    const {subcategoryId} = useParams();
    const request = {
        Title : title,
        Content : content,
        SubCategoryId : subcategoryId,
        UserId : user.id
    }
    
    const handleAddPost = () => {
        if (title.trim() !== '') {
            axiosAuth.post(ENDPOINTS.addPost, request)
                .then(() => {
                    onAddPostSubmit();
                })
                .catch(error => {
                    console.log('Blad: ', error);
                })
        }
    }

    return (
        <Box>
            <TextField
                multiline
                rows={4}
                variant="outlined"
                label="Tytuł"
                value={title}
                onChange={(event) => setNewTitle(event.target.value)}
                fullWidth
                margin="normal"
            />
            <TextField
                multiline
                rows={4}
                variant="outlined"
                label="Treść"
                value={content}
                onChange={(event) => setNewContent(event.target.value)}
                fullWidth
                margin="normal"
            />
            <Button variant="contained" color="primary" onClick={handleAddPost} fullWidth>
                Wyślij
            </Button>
        </Box>
    );
};

export default PostEditor;