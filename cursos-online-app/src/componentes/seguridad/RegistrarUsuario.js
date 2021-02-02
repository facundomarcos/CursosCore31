import React from 'react';
import {Container, Typography, Grid, TextField} from '@material-ui/core';

const style = {
    paper : {
        marginTop : 8,
        display : "flex",
        flexDirection : "column",
        alignItems : "center"
    },
    form: {
        width : "100%",
        marginTop :  20
    }
}

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
                            <TextField name="nombre" variant="outlined" fullWidth label="Ingrese su nombre"/>

                        </Grid>

                    </Grid>
                </form>
            </div>
        </Container>
    );

}

export default RegistrarUsuario;
