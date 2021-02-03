import React from 'react';
import {Container, Typography, Grid, TextField, Button} from '@material-ui/core';
import style from '../Tool/Style';

const RegistrarUsuario = () => {
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
                            <TextField name="nombre" variant="outlined" fullWidth label="Ingrese su Nombre"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="apellidos" variant="outlined" fullWidth label="Ingrese sus Apellidos"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="email" variant="outlined" fullWidth label="Ingrese su email"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="username" variant="outlined" fullWidth label="Ingrese su Username"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="password" type="password" variant="outlined" fullWidth label="Ingrese Password"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="confirmacionpassword" type="password" variant="outlined" fullWidth label="Confirme Password"/>
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
