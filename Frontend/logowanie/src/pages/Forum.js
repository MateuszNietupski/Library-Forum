import axios from "axios";
import {ListItem,ListItemText,Collapse,List,Card,CardContent,Typography } from "@mui/material";
import React,{useEffect,useState} from "react";
import Post from "./Post";
import PostList from "./PostList";
import {ENDPOINTS} from "../utils/consts";
import {BrowserRouter as Router,Route,Routes,Link} from "react-router-dom";

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

    const [openSubcategories, setOpenSubcategories] = React.useState({});

    const handleToggleSubcategory = (category) => {
        setOpenSubcategories((prevOpen) => ({
            ...prevOpen,
            [category]: !prevOpen[category],
        }));
    };

    return (
        
            <div>
                {categories.map((category) => (
                    <Card key={category.id} style={{margin: '10px 0'}}>
                        <CardContent>
                            <Typography variant="h5" component="div">
                                {category.name}
                            </Typography>
                            <List>
                                {category.subcategories && category.subcategories.map((subcategory) => (
                                    <ListItem key={subcategory.name} button>
                                        <ListItemText primary={subcategory.name}/>
                                    </ListItem>
                                ))}
                            </List>
                        </CardContent>
                    </Card>
                ))}
                <Routes>
                   // <Route path="/subcategories/:subcategoryId" component={PostList} />
                   // <Route path="/posts/:postId" component={Post} />
                </Routes>
            </div>
    );
}

export default Forum;