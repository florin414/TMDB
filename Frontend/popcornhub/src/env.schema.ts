import { z } from "zod"; 

const enviromentSchema = z.object({
    POPCORNHUB_API_URL: z.string().url(),
});

export const env = enviromentSchema.parse(import.meta.env);