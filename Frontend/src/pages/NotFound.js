import {Container,Box,Typography} from "@mui/material";

const NotFound = () => {
    
    return (
      <Container>
          <Box
              display="flex"
              flexDirection="column"
              alignItems="center"
              justifyContent="center"
              textAlign="center"
              sx={{mt:10}}
          >
              <Typography variant="h1" component="h1" gutterBottom>
                 Error 404
              </Typography>
              <Typography variant="h5" component="h2" gutterBottom>
                  Page Not found
              </Typography>
              <Typography variant="body1">
                  Strona, której szukasz, nie istnieje.
              </Typography>
          </Box>
      </Container>  
    );
}
export default NotFound;