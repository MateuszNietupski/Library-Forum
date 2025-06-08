import React, {useEffect, useMemo, useState} from "react";
import {ENDPOINTS} from "../utils/consts";
import axios from "axios";
import {Container, Grid, Switch} from "@mui/material";
import BreadCrumbsComponent from "../components/BreadCrumbComponent";
import {BookCard} from "../components/BookCard";
import FormControlLabel from "@mui/material/FormControlLabel";

const BooksPage = () => {
    const [books,setBooks] = useState([]);
    const [showAvailableOnly, setShowAvailableOnly] = useState(false);
    
    useEffect(() => {
        axios.get(ENDPOINTS.getBooks)
            .then(response => {
                setBooks(response.data);
            })
            .catch(error => {
               console.log('Blad pobierania danych', error) 
            });
    }, [books]);
    
    const visibleBooks = useMemo(
        () => showAvailableOnly ?  books.filter(book => book.quantity > 0) : books,
        [books,showAvailableOnly]
    )
    
    return(
        <Container sx={{mt:2}}>
            <BreadCrumbsComponent primaryName={"Katalog"}/>
            <FormControlLabel
                control={
                    <Switch
                        checked={showAvailableOnly}
                        onChange={e => setShowAvailableOnly(e.target.checked)}
                        color="primary"
                    />
                }
                label="Tylko aktualnie dostępne"
            />
            <Grid container spacing={3}>
                {visibleBooks ? visibleBooks.map((book) => (
                    <Grid item xs={12} sm={6} md={4} lg={3} key={book.id}>
                        <BookCard book={book}/>
                    </Grid>
                )) : (<p>Brak książek</p>)}
            </Grid>
        </Container>
    );
    
}
export default BooksPage;