import React, {useCallback, useEffect, useState} from "react";
import {Avatar, Box, Paper,Typography,Button,Container} from "@mui/material";
import CommentList from "../components/CommentList";
import {useParams} from "react-router-dom";
import axios from "axios";
import {ENDPOINTS} from "../utils/consts";
import CommentEditor from "../components/CommentEditor";
import {DateFormat} from "../utils/DateFormat";
import BreadCrumbsComponent from "../components/BreadCrumbComponent";

const Post = () => {
    const [post,setPost] = useState()
    const {categoryId,subcategoryId,postId} = useParams();
    const [isLoading,setIsLoading] = useState(true);
    const [isAddCommentVisible,setIsAddCommentVisible] = useState(false);
    const toggleAddCommentVisible = () => {
        setIsAddCommentVisible(!isAddCommentVisible);
    }
    const fetchData = useCallback(() => {
        axios.get(ENDPOINTS.getPost,{
            params: {postId}
        })
            .then(response => {
                setPost(response.data);
                setIsLoading(false);
            }).catch(err => {
            console.log('Blad pobierania danych', err)
        });
    },[postId]);
    useEffect(() => {
        fetchData();
    },[fetchData])
    
    return(
        !isLoading && (
            <Container>
                <BreadCrumbsComponent
                breadcrumbs={[{url: "/forum",name: "Kategorie"},{url: `/forum/${categoryId}/subcategory/${subcategoryId}`,name: "Posts"}]}    
                primaryName={"Post"}/>
                <Paper elevation={3} display="flex" alignItems="center" justifyContent="space-between" sx={{p:6,m:4}}>
                    <Box marginTop="20px">
                        <Typography variant="h4">{post.title}</Typography>
                    </Box>
                    <Box sx={{
                        textAlign: 'center',
                        mt: 5,
                        mb: 2,
                        wordWrap: 'break-word',
                        overflowWrap: 'break-word',
                        wordBreak: 'break-word',
                        width: '100%'  }}>
                        <Typography variant="body1" textAlign="center" mt={5} mb={2}>
                            {post.content}
                        </Typography>
                    </Box>
                    <Box display="flex">
                            <Box display="flex" alignItems="center" flexDirection="column">
                                <Avatar />
                                <Box marginTop="10px">
                                    {post.userName ? (
                                        <Typography variant="subtitle1">{post.userName}</Typography>
                                    ) : (
                                        <Typography variant="subtitle1">Użytkownik usunięty</Typography>
                                    )}
                                </Box>
                            </Box>
                        <Box flexGrow={1} textAlign="right">
                            <Typography variant="caption">{DateFormat(post.data)}</Typography>
                        </Box>
                    </Box>
                </Paper>
                <Box sx={{pr:8,pl:8}}>
                    {isAddCommentVisible ?
                        (
                            <>
                                <CommentEditor onCommentSubmit={() => {
                                toggleAddCommentVisible();
                                fetchData();
                                }}/>
                                <Button onClick={toggleAddCommentVisible} variant="outlined" fullWidth>Anuluj</Button>
                            </>
                        ) :
                        <Button onClick={toggleAddCommentVisible} variant="contained" fullWidth>
                            Dodaj komentarz
                        </Button>
                    }
                </Box>
                <CommentList comments={post.comments}/>
            </Container>
        )
    );
}
export default Post;