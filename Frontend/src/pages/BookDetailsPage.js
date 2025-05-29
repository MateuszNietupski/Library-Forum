import {Box, CardMedia, Container, Modal, Paper, Typography} from "@mui/material";
import {baseUrl, ENDPOINTS, PATHS} from "../utils/consts";
import {BrokenImage} from "@mui/icons-material";
import {useLocation, useParams} from "react-router-dom";
import React, {useEffect, useState} from "react";
import axios from "axios";
import BreadCrumbsComponent from "../components/BreadCrumbComponent";

const BookDetailsPage = () => {
    const { id } = useParams();
    const location = useLocation();
    const bookFromState = location.state?.book;
    const[book,setBook] = useState(bookFromState || null);
    const [loading, setLoading] = useState(!bookFromState);
    const [error, setError] = useState(null);
    const [openModal, setOpenModal] = useState(false);

    useEffect(() => {
        if(!book && id){
            setLoading(true);
            axios.get(ENDPOINTS.getBook)
                .then(response => {
                    setBook(response.data);
                    setLoading(false);
                })
                .catch(error => {
                    console.log('Blad pobierania danych', error)
                    setLoading(false);
                })
        }
    }, [id,book]);
    if (loading) {
        return <Container>Ładowanie...</Container>;
    }
    if (error) {
        return <Container>Błąd: {error}</Container>;
    }
    if (!book) {
        return <Container>Nie znaleziono książki</Container>;
    }
    const imageUrl = book.images?.length > 0
        ? baseUrl + book.images[0].filePath
        : null;
    
    return(
        <Container>
            <BreadCrumbsComponent
                breadcrumbs={[{url:PATHS.books,name: "Katalog"}]}
                primaryName={`${book.name}`}/>
            <Paper sx={{ p: 3, my: 3 }}>
                <Box sx={{ display: 'flex', flexDirection: { xs: 'column', md: 'row' }, gap: 3 }}>
                    <Box sx={{ width: { xs: '100%', md: '30%' } }}>
                        {imageUrl ? (
                            <CardMedia
                                component="img"
                                height="300"
                                image={imageUrl}
                                alt={book.title}
                                sx={{ objectFit: 'cover' }}
                                onClick={() => setOpenModal(true)}
                                />
                            ) : (<CardMedia
                                component={BrokenImage}
                                sx={{ height: 300, width: "100%", display: 'flex', alignItems: 'center', justifyContent: 'center', bgcolor: 'grey.200' }}
                                />
                        )}
                        </Box>
                    <Box sx={{ flex: 1 }}>
                        <Typography variant="h4" gutterBottom>{book.name}</Typography>
                        <Typography variant="h6" color="text.secondary" gutterBottom>
                            Autor: {book.author}
                        </Typography>
                        <Typography variant="h6" color="text.secondary" gutterBottom>
                            Gatunek: {book.category}
                        </Typography>
                        <Typography variant="body1" paragraph>
                            {book.description}
                        </Typography>
                    </Box>
                </Box>
            </Paper>
            <Modal
                open={openModal}
                onClose={() => setOpenModal(false)}
                aria-labelledby="modal-image"
                aria-describedby="full-size-image"
            >
                <Box sx={{
                    position: 'absolute',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    maxWidth: '90vw',
                    maxHeight: '90vh',
                    boxShadow: 24,
                    p: 1,
                }}>
                    <img
                        src={imageUrl}
                        alt={book.title}
                        style={{
                            maxWidth: '100%',
                            maxHeight: '100%',
                            objectFit: 'contain'
                        }}
                        onClick={() => setOpenModal(false)}
                    />
                </Box>
            </Modal>
        </Container>
    );
}
export default BookDetailsPage;