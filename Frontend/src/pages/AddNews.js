import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { ENDPOINTS } from '../utils/consts';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import axiosAuth from '../utils/authInstance';
import { generatePath } from 'react-router-dom';

const AddNews = () => {
    const [news, setNews] = useState([]);
    const [newNews, setNewNews] = useState({ title: '', content: '', description: '' });

    const handleNewNewsChange = (e) => {
        setNewNews({ ...newNews, [e.target.name]: e.target.value });
    }


    const handleAddSubmit = () => {
        axiosAuth.post((generatePath(ENDPOINTS.ADDNEWS, newNews)))
            .then(response => {
                setNews([ ...news, { ...newNews, id: response.data.id }]);
                setNewNews({ title: '', content: '', description: '' });
            })
            .catch(error => {
                console.error('There was an error adding the news!', error);
            });

    }
    return(
        <Container component="main" maxWidth="xs">
        <Box
            sx={{
                marginTop: 8,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}
        >
            <Typography component="h1" variant="h5">
                Dodaj nowy news
            </Typography>
            <Box component="form" noValidate sx={{ mt: 3 }}>
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextField
                            name="title"
                            required
                            fullWidth
                            id="newsTitle"
                            label="Tytuł"
                            value={newNews.title}
                            onChange={handleNewNewsChange}
                            autoFocus
                            sx={{
                            '& .MuiOutlinedInput-root': {
                                '&.Mui-focused fieldset': {
                                borderColor: '#785a48',
                                },
                            },
                            '& .MuiFormLabel-root': {
                                '&.Mui-focused': {
                                color: '#785a48',
                                },
                            },
                            }}
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <TextField
                            name="content"
                            required
                            fullWidth
                            multiline
                            rows={6}
                            id="newsContent"
                            label="Skrócony opis"
                            value={newNews.content}
                            onChange={handleNewNewsChange}
                            sx={{
                            '& .MuiOutlinedInput-root': {
                                '&.Mui-focused fieldset': {
                                borderColor: '#785a48',
                                },
                            },
                            '& .MuiFormLabel-root': {
                                '&.Mui-focused': {
                                color: '#785a48',
                                },
                            },
                            }}
                         />
                    </Grid>

                    <Grid item xs={12}>
                        <TextField
                            name="description"
                            required
                            fullWidth
                            multiline
                            rows={6}
                            id="newsDescription"
                            label="Opis"
                            value={newNews.description}
                            onChange={handleNewNewsChange}
                            sx={{
                            '& .MuiOutlinedInput-root': {
                                '&.Mui-focused fieldset': {
                                borderColor: '#785a48',
                                },
                            },
                            '& .MuiFormLabel-root': {
                                '&.Mui-focused': {
                                color: '#785a48',
                                },
                            },
                            }}
                        />
                    </Grid>
                </Grid>
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2, backgroundColor: "#785a48" }}
                    onClick={handleAddSubmit}
                >
                    Dodaj
                </Button>
            </Box>
        </Box>
        </Container>
    );
}
export default AddNews;