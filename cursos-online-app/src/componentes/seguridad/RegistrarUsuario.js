import React, {useState} from 'react';
import {Container, Typography, Grid, TextField, Button} from '@material-ui/core';
import style from '../Tool/Style';

const RegistrarUsuario = () => {
        //variable de estado para registrar en el backend
        const [usuario, setUsuario] = useState({
            //inicializamos todos los campos vacios
            //los nombres deben coincidir con los nombre de las cajas de texto
            NombreCompleto : '',
            Email : '',
            Password : '',
            ConfirmarPassword: '',
            Username: ''

        })


    return(
        //md para visualizar tamaño maximo en computadora
        //xs smartphone
        //sm tablet
        <Container component="main" maxWidth="md" justify="center">

            <div style={style.paper}>
               
                <Typography component="h1" variant="h5">
                    Registro de Usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={6}>
                            <TextField name="NombreCompleto" variant="outlined" fullWidth label="Ingrese su Nombre y Apellidos"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Email" variant="outlined" fullWidth label="Ingrese su email"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Username" variant="outlined" fullWidth label="Ingrese su Username"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Password" type="password" variant="outlined" fullWidth label="Ingrese Password"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="ConfirmarPassword" type="password" variant="outlined" fullWidth label="Confirme Password"/>
                        </Grid>

                    </Grid>
                    <Grid container justify="center">
                        <Grid item xs={12} md={6}>
                            <Button type="submit" fullWidth variant="contained" color="primary" size="large" style={style.submit}>
                                Enviar
                            </Button>
                        </Grid>

                    </Grid>
                </form>
            </div>
        </Container>
    );

}

export default RegistrarUsuario;
