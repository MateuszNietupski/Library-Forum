import React, {useEffect, useState} from "react";
import {Avatar, Box, Card, CardContent, Paper} from "@mui/material";
import Typography from "@mui/material/Typography";
import CommentList from "../components/CommentList";
import {useParams} from "react-router-dom";
import axios from "axios";
import {ENDPOINTS} from "../utils/consts";
import CommentEditor from "../components/CommentEditor";

const Post = () => {
    
    const [Post,setPost] = useState()
    const { categoryId,subcategoryId,postId } = useParams();
    const [isLoading,setIsLoading] = useState(true);
    useEffect(() => {
        axios.get(ENDPOINTS.getPost,{
            params: {postId}
        })
            .then(response => {
                setPost(response.data);
                setIsLoading(false);
            }).catch(err => {
            console.log('Blad pobierania danych', err)
        });
    }, [postId])
    
    return(
        
            isLoading ? null : (
                <Paper>
                    <Paper elevation={3} style={{ padding: '10px', width: '100%' }}>
                        <Box display="flex" alignItems="center" justifyContent="space-between">
                            <Box display="flex" alignItems="center" flexDirection="column">
                                <Box marginTop="10px">
                                    <Typography variant="title">{Post.title}</Typography>
                                </Box>
                            </Box>
                            <Box flex="1">
                                <Typography variant="body1" align="center">
                                    {Post.content}
                                </Typography>
                            </Box>
                            <Box>
                                <Typography variant="caption">{Post.data.toLocaleString()}</Typography>
                            </Box>
                        </Box>
                    </Paper> 
                    <CommentEditor/>
                    <CommentList comments={Post.comments}/>
                </Paper>
                
            )
    );

}
                

export default Post;