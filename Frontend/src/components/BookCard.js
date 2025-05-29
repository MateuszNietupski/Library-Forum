import {Card, CardContent, CardMedia, Typography,Button} from "@mui/material";
import {baseUrl} from "../utils/consts";
import {BrokenImage} from "@mui/icons-material";
import {useItemCart} from "../context/ItemCartProvider";
import { Link } from 'react-router-dom';

export const BookCard = ({book}) => {
    const imageUrl = book.images?.length > 0
        ? baseUrl + book.images[0].filePath
        : null;
    const { addItemToCart } = useItemCart();
    
    return (
        <Link
            to={`/books/${book.id}`}    
            state={{ book }}
            style={{ textDecoration: 'none', color: 'inherit' }}
            >
            <Card
                sx={{ maxWidth: 345, minHeight: 380, display: 'flex', flexDirection: 'column', justifyContent: 'space-between', m: 1,boxShadow: 3 }}>
                {imageUrl ? (<CardMedia
                    component="img"
                    height="180"
                    image={imageUrl}
                    alt={book.title}
                    sx={{ objectFit: 'cover' }}
                            />
                ) : (
                    <CardMedia
                        component={BrokenImage}
                        sx={{ height: 180,width:"100%", display: 'flex', alignItems: 'center', justifyContent: 'center', bgcolor: 'grey.200' }}
                    />
                )}
                <CardContent  sx={{
                    fontWeight: 'bold',
                    overflow: 'hidden',
                    whiteSpace: 'nowrap',
                    textOverflow: 'ellipsis',
                    fontSize: { xs: '1rem', md: '1.25rem' }
                }}>
                    <Typography variant="h6" sx={{fontWeight:'bold'}}>Tytuł: {book.name}</Typography>
                    <Typography variant="body" color="text.secondary" sx={{ display: 'block' }}>Autor: {book.author}</Typography>
                    <Typography variant="body" color="text.secondary" sx={{ display: 'block' }}>Gatunek: {book.category}</Typography>
                    <Button variant="contained" onClick={() => addItemToCart(book)} sx={{m:1}}>Dodaj do koszyka</Button>
                </CardContent>
            </Card>
        </Link>
    )
}