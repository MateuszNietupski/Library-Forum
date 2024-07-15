import {Container,Typography,Box} from "@mui/material";

const Unauthorized = () => {
    return (
        <Container>
            <Box
                display="flex"
                flexDirection="column"
                alignItems="center"
                justifyContent="center"
                textAlign="center"
                sx={{mt:10}}>
                <Typography variant="h1" gutterBottom>Unauthorized</Typography>
                <Typography variant="h5" gutterBottom>Nie masz dostępu do tej sekcji.</Typography>
            </Box>
        </Container>
    )
}
export default Unauthorized