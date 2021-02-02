import React from 'react';
import {Container, Typography} from '@material-ui/core';

const style = {
    paper : {
        marginTop : 8,
        display : "flex",
        flexDirection : "column",
        alignItems : "center"
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
            </div>
        </Container>
    );

}

export default RegistrarUsuario;
