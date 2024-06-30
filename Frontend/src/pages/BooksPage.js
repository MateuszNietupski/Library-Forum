import {useEffect, useState} from "react";
import {ENDPOINTS} from "../utils/consts";
import axios from "axios";
import Container from "@mui/material/Container";
import {Paper} from "@mui/material";
import Grid from "@mui/material/Grid";
import {useItemCart} from "../context/ItemCartProvider";
import Button from "@mui/material/Button";

const BooksPage = () => {
    const [books,setBooks] = useState([]);
    const { addItemToCart } = useItemCart();
    
    useEffect(() => {
        axios.get(ENDPOINTS.getBooks)
            .then(response => {
                setBooks(response.data);
            })
            .catch(error => {
               console.log('Blad pobierania danych', error) 
            });
    }, []);
    
    return(
      
        <div>
            <Container>
                {books.map((book) => (
                    <Grid item xs={4} key={book.id}>
                        <Paper>
                            {'Nazwa: ' + book.name}
                            {' Autor: ' + book.author}
                            <Button onClick={() => addItemToCart(book)}>
                                Wypożycz
                            </Button>
                        </Paper>
                    </Grid>
                ))}
            </Container>
            
        </div>
    );
    
}

export default BooksPage;