import React, { useState } from 'react';
import { TextField, Button, Box } from '@mui/material';
import axiosAuth from "../utils/authInstance";
import {ENDPOINTS} from "../utils/consts";
import {jwtDecode} from "jwt-decode";
import {useParams} from "react-router-dom";
import axios from "axios";

const CommentEditor = ({ onAddComment }) => {
    const [newComment, setNewComment] = useState('');
    const token = localStorage.getItem('access_token');
    const { categoryId,subcategoryId,postId } = useParams();
    let userId;
    if(token)
    {
        const decodeToken = jwtDecode(token);
        userId = decodeToken.Id
    }
    const handleCommentChange = (event) => {
        setNewComment(event.target.value);
    };
    const request = {
        Content: newComment,
        UserId: userId,
        PostId: postId
    }
    
    
    const handleAddComment = () => {
        if (newComment.trim() !== '') {
            axios.post(ENDPOINTS.addComment, {
                Content: newComment,
                UserId: userId,
                PostId: postId
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
                label="Dodaj nowy komentarz"
                value={newComment}
                onChange={(event) => setNewComment(event.target.value)}
                fullWidth
                margin="normal"
            />
            <Button variant="contained" color="primary" onClick={handleAddComment} fullWidth>
                Dodaj komentarz
            </Button>
        </Box>
    );
};

export default CommentEditor;
