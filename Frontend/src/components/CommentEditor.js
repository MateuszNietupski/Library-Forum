import React, {useContext, useState} from 'react';
import { TextField, Button, Box } from '@mui/material';
import axiosAuth from "../utils/authInstance";
import {ENDPOINTS} from "../utils/consts";
import {useParams} from "react-router-dom";
import {AuthContext} from "../context/AuthProvider";

const CommentEditor = ({onCommentSubmit}) => {
    const [newComment, setNewComment] = useState('');
    const {user} = useContext(AuthContext);
    const {postId} = useParams();
    const request = {
        Content: newComment,
        UserId: user.id,
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
