import axios from "axios";
import {ListItem,ListItemText,Collapse,List,Card,CardContent,Typography } from "@mui/material";
import React,{useEffect,useState} from "react";
import Post from "./Post";
import PostList from "./PostList";
import {ENDPOINTS} from "../utils/consts";
import {BrowserRouter as Router,Route,Routes,Link} from "react-router-dom";
import Grid from "@mui/material/Grid";
import Button from "@mui/material/Button";

const Forum = () => {
    
    const [categories,setCategories] = useState([]);

    useEffect(() => {
        axios.get(ENDPOINTS.getForumCategories)
            .then(response => {
                setCategories(response.data);
            })
            .catch(error => {
               console.log('Blad pobierania danych', error) 
            });
    }, []);
    

    return (
        
            <div>
                {categories.map((category) => (
                    <Card key={category.id} style={{margin: '10px 0'}}>
                        <CardContent>
                            <Typography variant="h5" component="div">
                                {category.name}
                            </Typography>
                            <Grid container>
                                {category.subcategories && category.subcategories.map((subcategory) => (
                                    <Grid item xs={10} sm={5} md={3} lg={2} key={subcategory.name} button>
                                        <Button 
                                            component={Link}
                                            to={`/forum/${category.id}/subcategory/${subcategory.id}`}>
                                            {subcategory.name}
                                        </Button>
                                        
                                    </Grid>
                                ))}
                            </Grid>
                        </CardContent>
                    </Card>
                ))}
            </div>
    );
}

export default Forum;