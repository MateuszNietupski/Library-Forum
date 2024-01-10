import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useState } from 'react';
import { ENDPOINTS } from '../utils/consts';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { PATHS } from '../utils/consts';
import { useNavigate } from 'react-router-dom';
import * as Yup from 'yup';
import {useFormik} from "formik";


export default function Register() {
  
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  
  const validationSchema = Yup.object({
    name: Yup.string().required('Pole wymagane'),
    email: Yup.string().email('Nieprawidłowy adres email').required('Pole wymagane'),
    password: Yup.string()
        .required('Pole wymagane')
        .min(8,'Hasło musi mieć conajmniej 8 znaków')
        .matches(/[A-Z]/, 'Hasło musi zawierać co najmniej jedną dużą literę')
        .matches(/[!@#$%^&*(),.?":{}|<>]/, 'Hasło musi zawierać co najmniej jeden znak specjalny')
        .matches(/[0-9]/, 'Haslo musi zawierac co najmniej jedną literę')
  });
  
  const formik = useFormik({
    initialValues: {
      name: '',
      email: '',
      password: ''
    },
    validationSchema,
    onSubmit: async (values) => {
      axios.post(ENDPOINTS.register,values).then(response =>{
        if (response.data.result) {
          navigate(PATHS.login);
        } else {
          setErrors(response.data.errors);
          alert(response.data.errors);
        }
      })
          .catch(error => {

            console.error('There was an error!', error);
          });
    }
  });

  return (
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Rejestracja
          </Typography>
          <Box component="form" noValidate onSubmit={formik.handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  autoComplete="given-name"
                  name="name"
                  required
                  fullWidth
                  id="name"
                  label="Name"
                  value={formik.values.name} 
                  onChange={formik.handleChange}
                  error={formik.touched.name && Boolean(formik.errors.name)}
                  helperText={formik.touched.name && formik.errors.name}
                  autoFocus
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  value={formik.values.email}
                  onChange={formik.handleChange}
                  error={formik.touched.email && Boolean(formik.errors.email)}
                  helperText={formik.touched.email && formik.errors.email}
                  autoComplete="email"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  value={formik.values.password}
                  onChange={formik.handleChange}
                  error={formik.touched.password && Boolean(formik.errors.password)}
                  helperText={formik.touched.password && formik.errors.password}
                  autoComplete="new-password"
                />
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Utwórz konto
            </Button>

            <Grid container justifyContent="flex-end">
              <Grid item>
                <Link href={PATHS.login} variant="body2">
                  Masz konto? Zaloguj się.
                </Link>
              </Grid>
            </Grid>
            {errors.length > 0 && (
              <div>
                <h2>Błąd:</h2>
                <ul>
                  {errors.map((error, index) => (
                    <li key={index}>{error}</li>
                  ))}
                </ul>
              </div>
            )}
          </Box>
        </Box>
      </Container>
  );
}