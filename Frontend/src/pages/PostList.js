import Grid from "@mui/material/Grid";
import React, {useCallback, useEffect, useState} from "react";
import {CardContent,Typography,Button,Card,CardActions} from "@mui/material";
import {Link, useParams} from "react-router-dom";
import axios from "axios";
import {ENDPOINTS} from "../utils/consts";
import PostEditor from "../components/PostEditor";
import Container from "@mui/material/Container";
import Box from "@mui/material/Box";
import BreadCrumbsComponent from "../components/BreadCrumbComponent";

const PostList = () => {
    const [isLoading,setIsLoading] = useState(true);
    const [isAddPostVisible,setIsAddPostVisible] = useState(false);
    const [Posts,setPosts] = useState([])
    const { categoryId,subcategoryId } = useParams();
    const toggleAddPostVisible = () => {
        setIsAddPostVisible(!isAddPostVisible);
    }
    const fetchData = useCallback(() => {
        axios.get(ENDPOINTS.getForumPosts,{
            params: {subcategoryId}
        })
            .then(response => {
                setPosts(response.data);
                setIsLoading(false);
            }).catch(err => {
            console.log('Blad pobierania danych', err)
        });
    },[subcategoryId])
    useEffect(() => {
        fetchData();
    }, [fetchData]);

    return(
        <Container>
            <Typography variant="h3" m={5}>
                Kategoria:
            </Typography>
            <BreadCrumbsComponent 
                breadcrumbs={[{url: "/forum",name: "Kategorie"}]}
                primaryName={"Posts"}/>
            <Grid container spacing={3}>
                {isLoading ? 
                    <Typography>Ładowanie...</Typography> : 
                    Posts && Posts.length > 0 ? (Posts.map((post) => (
                        <Grid item xs={12} sm={6} md={4} key={post.id}>
                            <Card>
                                <CardContent>
                                    <Typography variant="h5" component="div">
                                        {post.title}
                                    </Typography>
                                    <Typography variant="body2" color="text.secondary">
                                        {post.content.length < 100 ? post.content : `${post.content.substring(0, 50)}...`}
                                    </Typography>
                                </CardContent>
                                <CardActions>
                                    <Button size="small"
                                            component={Link}
                                            to={`/forum/${categoryId}/subcategory/${subcategoryId}/post/${post.id}`}>
                                        Czytaj więcej
                                    </Button>
                                </CardActions>
                            </Card>
                        </Grid>
                        ))) : (
                            <Typography>Brak postów</Typography>
                )}
            </Grid>
            <Box sx={{mb:10,mt:2,p:5}}>
                {isAddPostVisible ? (
                    <PostEditor onAddPostSubmit={() => {
                        toggleAddPostVisible();
                        fetchData();
                    }}/>) : null}
                <Button variant="outlined" fullWidth onClick={toggleAddPostVisible}>
                    {isAddPostVisible ? 'Anuluj' : 'Dodaj Post'}
                </Button>
            </Box>
        </Container>
    );
}
export default PostList;