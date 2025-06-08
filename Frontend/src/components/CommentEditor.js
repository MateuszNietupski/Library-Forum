import React, {useContext, useState} from 'react';
import { TextField, Button, Box } from '@mui/material';
import axiosAuth from "../utils/authInstance";
import {ENDPOINTS} from "../utils/consts";
import {useParams} from "react-router-dom";
import {AuthContext} from "../context/AuthProvider";
import axiosDefault from "../utils/defaultInstance";

const CommentEditor = ({onCommentSubmit,endpoint}) => {
    const [newComment, setNewComment] = useState('');
    const {user} = useContext(AuthContext);
    const {id} = useParams();
    const request = {
        Content: newComment,
        UserId: user.id,
    }
    
    const handleAddComment = () => {
        if (newComment.trim() !== '') {
            axiosDefault.post(endpoint, request)
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
