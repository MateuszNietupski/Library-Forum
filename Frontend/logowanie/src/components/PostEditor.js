import React, { useState } from 'react';
import { TextField, Button, Box } from '@mui/material';
import axiosAuth from "../utils/authInstance";
import {ENDPOINTS} from "../utils/consts";
import {jwtDecode} from "jwt-decode";
import {useParams} from "react-router-dom";
import axios from "axios";

const PostEditor = ({ onAddComment }) => {
    const [title, setNewTitle] = useState('');
    const [content, setNewContent] = useState('');
    const token = localStorage.getItem('access_token');
    const { categoryId,subcategoryId,postId } = useParams();
    let userId;
    if(token)
    {
        const decodeToken = jwtDecode(token);
        userId = decodeToken.Id
    }
  
    const request = {
        Title: title,
        Content: content,
        SubCategoryId: subcategoryId
    }


    const handleAddComment = () => {
        if (title.trim() !== '') {
            axios.post(ENDPOINTS.addPost, {
                Title: title,
                Content: content,
                SubCategoryId: subcategoryId
            })
                .then(console.log('ok'))
                .catch(error => {
                    console.log('Blad: ', error);
                })

        };
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
            <Button variant="contained" color="primary" onClick={handleAddComment} fullWidth>
                Dodaj Post
            </Button>
        </Box>
    );
};

export default PostEditor;