import React, { useState } from 'react';
import { TextField, Button, Box } from '@mui/material';
import axiosAuth from "../utils/authInstance";
import {ENDPOINTS} from "../utils/consts";
import {jwtDecode} from "jwt-decode";
import {useParams} from "react-router-dom";

const CommentEditor = ({onCommentSubmit}) => {
    const [newComment, setNewComment] = useState('');
    const token = localStorage.getItem('access_token');
    const {postId} = useParams();
    let userId;
    if(token)
    {
        const decodeToken = jwtDecode(token);
        userId = decodeToken.Id
    }
    const request = {
        Content: newComment,
        UserId: userId,
        PostId: postId
    }
    const handleAddComment = () => {
        if (newComment.trim() !== '') {
            axiosAuth.post(ENDPOINTS.addComment, request)
                .then(() => {
                    onCommentSubmit();
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
                label="Dodaj nowy komentarz"
                value={newComment}
                onChange={(event) => setNewComment(event.target.value)}
                fullWidth
                margin="normal"
            />
            <Button variant="contained" color="primary" onClick={handleAddComment} fullWidth>
                Wyślij
            </Button>
        </Box>
    );
};

export default CommentEditor;
