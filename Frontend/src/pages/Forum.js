import axios from "axios";
import {Card,CardContent,Typography } from "@mui/material";
import React,{useEffect,useState} from "react";
import {ENDPOINTS} from "../utils/consts";
import {Link} from "react-router-dom";
import Grid from "@mui/material/Grid";
import Button from "@mui/material/Button";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";

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
        <Container>
                {categories.map((category) => (
                    <Card key={category.id} sx={{m:4}}>
                        <CardContent style={{ padding: '16px' }}>
                            <Typography variant="h5" component="div">
                                {category.name}
                            </Typography>
                            <Grid container spacing={2}>
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
        </Container>
    );
}

export default Forum;